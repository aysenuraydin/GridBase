using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace gridbase.WebApi.Jobs;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly ILogger<RabbitMqConsumerService> _logger;

    public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var factory = new ConnectionFactory { HostName = "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "task_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            int dots = message.Split('.').Length - 1;
            await Task.Delay(dots * 1000);

            Console.WriteLine(" [x] Done");

            //! işlemler
            //DB, Email, PDF

            // channel.BasicAck yerine asenkron versiyonu
            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);//BasicAck bu yöntem, RabbitMQ'daki mesajın tüketiciden başarıyla işlendiğini doğrular ve mesajın kuyruktan silinmesini sağlar.

            //deliveryTag:bir mesajın benzersiz kimlik numarasıdır
            //multiple: birden fazla mesajı aynı anda onaylası mı?
        };

        await channel.BasicConsumeAsync(
            queue: "task_queue",
            autoAck: false,
            consumer: consumer
        );//kuyruğu dinlemeye başlıyoruz

        // Service sürekli çalışsın
        await Task.Delay(-1, stoppingToken);
    }
}


/* 
    !!!
    *Received / ReceivedAsync
    Kuyruğa bir mesaj geldiğinde tetiklenir. Mesajın işlenmesini buradan yaparsın.
    *Registered
    Consumer RabbitMQ’ya kaydolduğunda tetiklenir.
    *Unregistered
    Consumer RabbitMQ’dan kaydı silindiğinde tetiklenir.
    *Shutdown / ShutdownAsync
    Kanal veya bağlantı kapatıldığında tetiklenir.
    *ConsumerCancelled / ConsumerCancelledAsync
    Kuyrukta consumer iptal edildiğinde tetiklenir.
    *ConsumerTagChanged / ConsumerTagChangedAsync
    Consumer’ın tag’i değiştiğinde tetiklenir.

*/


/* 
! Controlerda


namespace gridbase.WebApi.Controllers;

[Route("api/[controller]")]
public class UserController : BaseController<User, Guid>
{
    public UserController(IService<User, Guid> service) : base(service)
    {
    }
    /*
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        //*1 ConnectionFactory: host tanımlamayı sağlar ve rabbitmq ile baglantı kurmak için kullanılır.
        var factory = new ConnectionFactory() { HostName = "localhost" };
        //*1 { HostName = "localhost", port = 5672 }; //port = 5672 varsayılan hostu kullandığımız için belirtmesekte oluyor

        //*CreateConnectionAsync: bir baplantı oluşturmayı sağlar (8.x sürümünde asenkron)
        await using var connection = await factory.CreateConnectionAsync();

        //*CreateChannelAsync: yani bir kanal oluşturur. bu kanal içerisinde queue oluşturup istenilen mesaj(içerik, nesne yi stringe dönüştürerek) göndeririz
        await using var channel = await connection.CreateChannelAsync();

        //*kuyruğu oluşturalım
        await channel.QueueDeclareAsync(
            queue: "deneme", //kuyruğun adı
            durable: false, //kuyruğun fiziksel olarak kalıcak mı? rabbitmq çalışırken hata olduğunda kuyruktaki işlemler devam edilsin mi?
            exclusive: false,//kuyruğun bir kere olusturulduktan sonra kapatılacak mı?
            autoDelete: false,//kuyruğun herhangi bir yerden kapatıldıktan sonra kuyruğun kalıcak mı?
            arguments: null//kuyruğun ek bilgileri
        );

        //*consumer tarafından işlem yapılacak olan kullanıcı bilgilerimizi Message olarak kuyruğa ekliyoruz. işlemin kendisini (veri tabanı işlemleri, notification , pdf oluşturması gibi) burada yapmıyoruz kuyruğa gönderiyoruz o yapıyor böylece projemizde performans kaybı olmuyor
        //* 1 den 50 liste oluşturup döngü ile 50 tane fake kullanıcı oluşturduk. gerçekte gerçek verileri kullanabiliriz.


        foreach (var x in Enumerable.Range(1, 50))
        {
            var user = new User
            {
                Id = new Guid(),
                Username = "Ali " + x,
                FirstName = "Veli " + x,
            };

            var message = JsonSerializer.Serialize(user); //user'ı string'e çevirdik.
            var body = Encoding.UTF8.GetBytes(message); //string'i byte array'e çevirdik.

            //* BasicPublishAsync: 8.x sürümünde async ve basicProperties parametresi zorunlu
            await channel.BasicPublishAsync(
                exchange: "", //exchange adı
                routingKey: "deneme", //kuyruğun adı consumerdaki adı da öyle olmalı
                mandatory: false, //mesaj bir kuyruğa yönlendirilemezse geri döner mi?
                basicProperties: new BasicProperties(), //en azından boş bir properties vermek zorunlu
                body: new ReadOnlyMemory<byte>(body) //mesajın içeriği
            );

            Console.WriteLine($"Mesaj gönderildi. {message}");
        }

        Console.WriteLine($"Gönderim tamamlandı");

        return Ok(new
        {
            message = "Kullanıcı kayıtları alınmıştır."
        });
    }
    /*
    [HttpPost("producer")]
    public async Task<IActionResult> Producer([FromBody] string message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
        await using var connection = await factory.CreateConnectionAsync();//CreateConnection yerine asenkron CreateConnectionAsync kullandık
        await using var channel = await connection.CreateChannelAsync();//CreateModel yerine CreateChannelAsync kullandık

        await channel.QueueDeclareAsync(
            queue: "task_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Web API'den gelen message doğrudan kullanıyoruz, artık GetMessage'e ihtiyaç yok
        var body = Encoding.UTF8.GetBytes(message);

        var properties = new BasicProperties { Persistent = true };//asenkron API’de CreateBasicProperties yok, doğrudan BasicProperties nesnesi oluşturduk

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: "task_queue",//consumerdaki ad ile aynı olmalı
            mandatory: false,
            basicProperties: properties,
            body: body
        );//BasicPublish yerine BasicPublishAsync kullandık
        Console.WriteLine($" [x] Sent {message}");//konsole bu yazılıcak

        //* dotnet run "A B C." "D. E." "F."
        //* => [x] Sent A B C. D. E. F.
        //* =>  Press [enter] to exit.
        return Ok(new { message = $" [x] Sent {message}" });
    }
}

//* docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
*/