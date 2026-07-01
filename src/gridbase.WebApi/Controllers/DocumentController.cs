using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Authorize(Roles = "GB")]
[Route("api/[controller]")]
public class DocumentController(IDocumentService service) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<DocumentDto>> Get() =>
        Ok(await service.GetAsync());

    [HttpPut]
    public async Task<ActionResult<DocumentDto>> Upsert(DocumentDto dto) =>
        Ok(await service.UpsertAsync(dto));
}






// <!DOCTYPE html>
// <html lang="tr">
// <head>
// <meta charset="UTF-8">
// <meta name="viewport" content="width=device-width, initial-scale=1.0">
// <title>GridBase — Geliştirici Dokümantasyonu</title>
// <style>
//   :root{
//     --bg:#ffffff; --panel:#f7f8fa; --panel-2:#eef1f5; --line:#e3e7ee;
//     --ink:#1a1f2b; --ink-soft:#4a5468; --ink-mute:#8a93a6;
//     --accent:#3b6fed; --accent-bg:#eaf0fe;
//     --get:#1a7f4b; --get-bg:#e7f4ec; --post:#3b6fed; --post-bg:#eaf0fe;
//     --put:#9a6b00; --put-bg:#fbf1dc; --patch:#6b3fc4; --patch-bg:#efe9fb;
//     --delete:#c0392b; --delete-bg:#fbe9e7;
//     --code-bg:#f6f8fa; --mark:#eef1f5;
//     --radius:10px; --maxw:880px;
//     --mono:"SF Mono","JetBrains Mono",Menlo,Consolas,monospace;
//     --sans:-apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,Helvetica,Arial,sans-serif;
//   }
//   *{box-sizing:border-box;margin:0;padding:0}
//   html{scroll-behavior:smooth}
//   body{background:var(--bg);color:var(--ink);font-family:var(--sans);font-size:15px;line-height:1.65;-webkit-font-smoothing:antialiased}
//   a{color:var(--accent);text-decoration:none}
//   a:hover{text-decoration:underline}
//   code{font-family:var(--mono);font-size:.88em}

//   .wrap{display:grid;grid-template-columns:280px 1fr;min-height:100vh}

//   /* SIDEBAR */
//   .side{position:sticky;top:0;height:100vh;overflow-y:auto;background:var(--panel);border-right:1px solid var(--line);padding:28px 18px 60px}
//   .side::-webkit-scrollbar{width:8px}
//   .side::-webkit-scrollbar-thumb{background:var(--line);border-radius:4px}
//   .brand{display:flex;align-items:center;gap:11px;padding:0 8px 22px;margin-bottom:14px;border-bottom:1px solid var(--line)}
//   .brand .logo{width:34px;height:34px;border-radius:8px;flex:none;background:linear-gradient(135deg,#3b6fed,#6b3fc4);display:grid;place-items:center;font-weight:800;color:#fff;font-size:17px;box-shadow:0 2px 8px rgba(59,111,237,.3)}
//   .brand .name{font-weight:700;font-size:16px;letter-spacing:-.2px}
//   .brand .sub{font-size:11.5px;color:var(--ink-mute);margin-top:1px}
//   .nav-group{margin-top:18px}
//   .nav-group h4{font-size:11px;text-transform:uppercase;letter-spacing:.9px;color:var(--ink-mute);padding:0 10px;margin-bottom:7px;font-weight:700}
//   .nav a{display:block;padding:6.5px 10px;border-radius:7px;color:var(--ink-soft);font-size:13.5px;line-height:1.3;transition:background .12s,color .12s}
//   .nav a:hover{background:var(--panel-2);color:var(--ink);text-decoration:none}
//   .nav a.active{background:var(--accent-bg);color:var(--accent);font-weight:600}

//   /* CONTENT */
//   .main{padding:0 0 120px}
//   .content{max-width:var(--maxw);margin:0 auto;padding:54px 40px 0}
//   section{scroll-margin-top:24px;padding-bottom:18px}
//   section + section{border-top:1px solid var(--line);padding-top:46px;margin-top:30px}
//   h1{font-size:34px;letter-spacing:-.8px;font-weight:800;line-height:1.15}
//   h2{font-size:25px;letter-spacing:-.5px;font-weight:750;margin-bottom:14px;scroll-margin-top:24px}
//   h3{font-size:17.5px;font-weight:700;margin:26px 0 10px;color:var(--ink)}
//   h4{font-size:14.5px;font-weight:700;margin:20px 0 8px;color:var(--ink-soft)}
//   p{margin:11px 0;color:var(--ink-soft)}
//   p strong,li strong{color:var(--ink);font-weight:650}
//   ul,ol{margin:11px 0 11px 4px;padding-left:22px;color:var(--ink-soft)}
//   li{margin:5px 0}
//   .lead{font-size:17px;color:var(--ink-soft);margin-top:14px}
//   .eyebrow{display:inline-block;font-size:12px;font-weight:700;letter-spacing:1.2px;text-transform:uppercase;color:var(--accent);margin-bottom:14px}

//   /* CODE */
//   pre{background:var(--code-bg);border:1px solid var(--line);border-radius:var(--radius);padding:16px 18px;overflow-x:auto;margin:14px 0;position:relative}
//   pre code{font-size:13px;line-height:1.7;color:#2d3340}
//   pre::-webkit-scrollbar{height:8px}
//   pre::-webkit-scrollbar-thumb{background:var(--line);border-radius:4px}
//   .tok-key{color:#0a7d3e}.tok-str{color:#1a56c4}.tok-num{color:#b5500a}.tok-com{color:#8a93a6;font-style:italic}.tok-punc{color:#5a6478}
//   p code,li code,td code{background:var(--mark);padding:1.5px 6px;border-radius:5px;color:#34405a;font-size:.86em;border:1px solid var(--line)}

//   /* ENDPOINT */
//   .endpoint{display:flex;align-items:center;gap:11px;background:var(--panel);border:1px solid var(--line);border-radius:9px;padding:10px 13px;margin:14px 0;font-family:var(--mono);font-size:13px;flex-wrap:wrap}
//   .verb{font-weight:700;font-size:11.5px;letter-spacing:.5px;padding:3px 9px;border-radius:5px;flex:none}
//   .verb.get{background:var(--get-bg);color:var(--get)}.verb.post{background:var(--post-bg);color:var(--post)}
//   .verb.put{background:var(--put-bg);color:var(--put)}.verb.patch{background:var(--patch-bg);color:var(--patch)}
//   .verb.delete{background:var(--delete-bg);color:var(--delete)}
//   .endpoint .path{color:var(--ink);word-break:break-all}

//   /* TABLE */
//   .tbl{width:100%;border-collapse:collapse;margin:16px 0;font-size:13.5px;border-radius:var(--radius);border:1px solid var(--line);overflow:hidden}
//   .tbl th{background:var(--panel-2);text-align:left;padding:10px 13px;font-weight:650;color:var(--ink);font-size:12.5px;border-bottom:1px solid var(--line)}
//   .tbl td{padding:9px 13px;border-bottom:1px solid var(--line);color:var(--ink-soft);vertical-align:top}
//   .tbl tr:last-child td{border-bottom:none}
//   .tbl tr:hover td{background:rgba(0,0,0,.012)}
//   .tbl code{white-space:nowrap}

//   /* CALLOUT */
//   .note{border-left:3px solid var(--accent);background:var(--panel);padding:13px 16px;border-radius:0 8px 8px 0;margin:16px 0;font-size:14px;color:var(--ink-soft)}
//   .note.warn{border-left-color:var(--put)}
//   .note.danger{border-left-color:var(--delete)}
//   .note.good{border-left-color:var(--get)}
//   .note strong{display:block;margin-bottom:3px;color:var(--ink)}
//   .pill{display:inline-block;font-size:11px;font-weight:700;padding:2px 8px;border-radius:20px;vertical-align:middle}
//   .pill.new{background:var(--get-bg);color:var(--get);border:1px solid rgba(26,127,75,.25)}

//   /* COPY */
//   .copy{position:absolute;top:9px;right:9px;background:var(--bg);border:1px solid var(--line);color:var(--ink-mute);font-size:11px;padding:4px 9px;border-radius:6px;cursor:pointer;font-family:var(--sans);transition:all .12s}
//   .copy:hover{color:var(--ink);border-color:var(--accent)}

//   /* MOBILE */
//   .menu-btn{display:none}
//   @media(max-width:880px){
//     .wrap{grid-template-columns:1fr}
//     .side{position:fixed;left:-300px;width:280px;z-index:50;transition:left .25s;box-shadow:4px 0 30px rgba(0,0,0,.15)}
//     .side.open{left:0}
//     .menu-btn{display:flex;position:fixed;top:14px;left:14px;z-index:60;width:42px;height:42px;background:var(--panel);border:1px solid var(--line);border-radius:9px;cursor:pointer;align-items:center;justify-content:center;color:var(--ink)}
//     .content{padding:70px 22px 0}
//     h1{font-size:27px}
//     .backdrop{position:fixed;inset:0;background:rgba(0,0,0,.3);z-index:40;display:none}
//     .backdrop.show{display:block}
//   }
// </style>
// </head>
// <body>

// <button class="menu-btn" id="menuBtn" aria-label="Menü">
//   <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="3" y1="6" x2="21" y2="6"/><line x1="3" y1="12" x2="21" y2="12"/><line x1="3" y1="18" x2="21" y2="18"/></svg>
// </button>
// <div class="backdrop" id="backdrop"></div>

// <div class="wrap">
//   <aside class="side" id="side">
//     <div class="brand">
//       <div class="logo">G</div>
//       <div><div class="name">GridBase</div><div class="sub">Geliştirici Dokümantasyonu</div></div>
//     </div>
//     <nav class="nav">
//       <div class="nav-group">
//         <h4>Başlangıç</h4>
//         <a href="#intro">Giriş</a>
//         <a href="#concepts">Temel Kavramlar</a>
//         <a href="#projects">Projeler</a>
//         <a href="#auth">Kimlik Doğrulama</a>
//         <a href="#keys">API Anahtarları</a>
//         <a href="#quickstart">Hızlı Başlangıç</a>
//       </div>
//       <div class="nav-group">
//         <h4>Veri İşlemleri</h4>
//         <a href="#crud">CRUD İşlemleri</a>
//         <a href="#query">Filtreleme & Sorgulama</a>
//         <a href="#relations">İlişkiler & Expand</a>
//         <a href="#validation">Doğrulama</a>
//         <a href="#storage">Storage (Dosyalar)</a>
//       </div>
//       <div class="nav-group">
//         <h4>Yönetim</h4>
//         <a href="#access">Tablo Erişim Ayarları</a>
//         <a href="#cors">CORS Ayarları</a>
//         <a href="#console">Console Kullanımı</a>
//         <a href="#errors">Hata Kodları</a>
//       </div>
//       <div class="nav-group">
//         <h4>İleri Düzey</h4>
//         <a href="#architecture">Mimari</a>
//       </div>
//     </nav>
//   </aside>

//   <main class="main">
//     <div class="content" id="content">
//       <!-- GİRİŞ -->
//       <section id="intro">
//         <span class="eyebrow">Genel Bakış</span>
//         <h1>GridBase</h1>
//         <p class="lead">Tablo tanımla, veri tut, REST API üzerinden tüket — sunucu kodu yazmadan. Çok kiracılı (multi-tenant) bir Backend-as-a-Service motoru.</p>
//         <h3>GridBase ne yapar?</h3>
//         <p>GridBase, Firebase ve Supabase'in çözdüğü problemi çözer: bir uygulamanın arka ucunu sıfırdan yazmak yerine hazır bir veri katmanı + REST API kullanırsın. Bir tablo oluşturursun, kolonlarını tanımlarsın (ya da veri eklerken otomatik açılır), ve anında o tabloya veri ekleyip okuyabileceğin uçlar elde edersin.</p>
//         <h3>Nasıl konumlanır?</h3>
//         <p>GridBase <strong>tek merkezi bir servistir</strong>. Tüm geliştiriciler aynı servise kaydolur; her projenin verisi GridBase'in veritabanında <strong>izole birer tenant</strong> olarak tutulur. Bir projenin verisi başka bir projeden hiçbir koşulda görülemez. Bu yönüyle Firebase'e benzer — ama Firebase'de olmayan bir şey sunar: <strong>sunucu taraflı doğrulama</strong> (zorunlu alanlar, sayı aralıkları, benzersizlik, ilişki bütünlüğü).</p>
//         <div class="note good">
//           <strong>Tek cümlede</strong>
//           Firebase'in yaptığı her şeyi yapar; ek olarak sunucu taraflı doğrulama sunar. İstersen görsel Console'dan, istersen doğrudan API'den kullanırsın — ikisi de aynı veriye bakar.
//         </div>
//       </section>

//       <!-- KAVRAMLAR -->
//       <section id="concepts">
//         <span class="eyebrow">Temeller</span>
//         <h2>Temel Kavramlar</h2>
//         <p>GridBase'i kullanmak için dört kavram yeterli.</p>
//         <h3>Proje (Tenant)</h3>
//         <p>Bir <strong>proje</strong> izole bir veri alanıdır — kendi tabloları, dosyaları ve API anahtarları olan bir kiracı. Tek bir geliştirici birden fazla proje açabilir; her proje diğerlerinden tamamen ayrıdır. Her istek bir projeye yöneliktir; bunu <code>X-Project-Id</code> başlığıyla ya da API anahtarıyla belirtirsin.</p>
//         <h3>Tablo</h3>
//         <p>Bir <strong>tablo</strong> aynı türden kayıtların tutulduğu yerdir — <code>products</code>, <code>todos</code> gibi. Kolonlar sabit değildir: veri eklerken yeni bir alan gönderirsen o kolon otomatik açılır. Şema değiştirmek için migration gerekmez.</p>
//         <h3>Satır ve Kolon</h3>
//         <p>API tarafında her kaydı tanıdık bir JSON nesnesi olarak görürsün:</p>
//         <pre><code>{
//   <span class="tok-key">"id"</span>: <span class="tok-num">5</span>,
//   <span class="tok-key">"title"</span>: <span class="tok-str">"Market alışverişi"</span>,
//   <span class="tok-key">"completed"</span>: <span class="tok-num">false</span>
// }</code></pre>
//         <h3>EAV: Perde arkası</h3>
//         <p>Yukarıdaki JSON sana normal bir kayıt gibi görünür. Ama GridBase bunu arka planda <strong>EAV (Entity-Attribute-Value)</strong> modeliyle saklar: gerçek anlamda bir <code>products</code> tablosu yoktur; her şey satır (row) ve hücre (cell) olarak tutulur. Bu sayede tablolar tamamen dinamiktir.</p>
//         <div class="note">
//           <strong>Sen bunu bilmek zorunda değilsin</strong>
//           API her zaman düz JSON döner ve düz JSON kabul eder. EAV yapısı tamamen gizlidir. Ayrıntı için <a href="#architecture">Mimari</a> bölümüne bak.
//         </div>
//         <h3>İki tür kullanıcı</h3>
//         <table class="tbl">
//           <tr><th>Tür</th><th>Kim</th><th>Nasıl erişir</th></tr>
//           <tr><td><strong>Developer</strong></td><td>Projeyi yöneten</td><td>Console'a giriş yapar; tablo/kolon/ilişki/erişim ayarlarını yönetir.</td></tr>
//           <tr><td><strong>End-user</strong></td><td>Uygulamayı kullanan</td><td>API anahtarıyla veriyi tüketir; tablonun erişim kurallarına tabidir.</td></tr>
//         </table>
//       </section>

//       <!-- PROJELER -->
//       <section id="projects">
//         <span class="eyebrow">Yönetim</span>
//         <h2>Projeler</h2>
//         <p>Console'a girdiğinde ilk gördüğün yer <strong>Projelerim</strong> ekranıdır. Her proje kendi tabloları, dosyaları ve API anahtarlarıyla izole bir alandır.</p>
//         <h3>Proje oluşturma</h3>
//         <p>"Yeni proje" kartına tıklar, ad ve (opsiyonel) açıklama verirsin. <strong>Free planda 2 proje</strong> hakkın vardır; ekranda kalan hakkın gösterilir.</p>
//         <h3>Proje seçme</h3>
//         <p>Bir projeye tıkladığında o proje aktif olur ve tablolarına yönlendirilirsin. Aktif proje, sonraki tüm isteklere otomatik olarak <code>X-Project-Id</code> başlığıyla eklenir.</p>
//         <h3>Proje ayarları</h3>
//         <p>Ayarlar ekranından projenin adını/açıklamasını düzenler, <a href="#cors">CORS</a> origin'lerini yönetir ya da projeyi silersin. Silme kalıcıdır: tüm tablolar, satırlar, dosyalar ve API anahtarları geri alınamaz biçimde gider (onay için proje adını yazman istenir).</p>
//       </section>

//       <!-- AUTH -->
//       <section id="auth">
//         <span class="eyebrow">Güvenlik</span>
//         <h2>Kimlik Doğrulama</h2>
//         <p>API'ye her istek iki bilgi taşır: <strong>hangi projeye</strong> gittiği ve <strong>kim olarak</strong> gittiği.</p>
//         <h3>İstek başlıkları</h3>
//         <p>Veri uçlarına yapılan her istek bir API anahtarı taşır:</p>
//         <pre><code>X-Project-Id: <span class="tok-str">YOUR_PROJECT_ID</span>
// X-GridBase-Key: <span class="tok-str">gb_pk_live_xxxxxxxxxxxx</span></code></pre>
//         <p>Geçerli bir anahtar gönderildiğinde proje otomatik çözülür.</p>
//         <h3>Developer girişi (JWT)</h3>
//         <p>Console'a giren ya da yönetim uçlarını (tablo oluşturma, erişim ayarı) kullanan geliştiriciler JWT ile kimlik doğrular.</p>
//         <div class="endpoint"><span class="verb post">POST</span><span class="path">/api/auth/authenticate</span></div>
//         <pre><code>{ <span class="tok-key">"email"</span>: <span class="tok-str">"developer@example.com"</span>, <span class="tok-key">"password"</span>: <span class="tok-str">"********"</span> }</code></pre>
//         <p>Dönen <code>accessToken</code>'ı sonraki yönetim isteklerinde <code>Authorization: Bearer &lt;token&gt;</code> olarak gönderirsin. Diğer auth uçları: <code>register</code>, <code>refresh-token</code>, <code>forgot-password</code>, <code>logout</code>.</p>
//       </section>

//       <!-- API ANAHTARLARI -->
//       <section id="keys">
//         <span class="eyebrow">Güvenlik</span>
//         <h2>API Anahtarları</h2>
//         <p>Her projenin iki tür anahtarı vardır. Bunları Console'daki <strong>API Anahtarları</strong> ekranından yönetirsin.</p>
//         <table class="tbl">
//           <tr><th>Anahtar</th><th>Önek</th><th>Nerede</th><th>Davranış</th></tr>
//           <tr><td><strong>Anon</strong></td><td><code>gb_pk_live_</code></td><td>Frontend</td><td>Tablonun erişim kurallarına <strong>tabidir</strong>. Güvenle istemciye konur.</td></tr>
//           <tr><td><strong>Secret</strong></td><td><code>gb_sk_live_</code></td><td>Yalnız backend</td><td>Tüm erişim kurallarını <strong>bypass eder</strong>.</td></tr>
//         </table>
//         <h3>Anahtar oluşturma</h3>
//         <p>"Yeni anahtar" → tip seç (anon / secret) → oluştur. Anahtar <strong>yalnızca bir kez gösterilir</strong> — o anda kopyalayıp güvenli bir yere kaydetmelisin, tekrar göremezsin. Listede yalnızca öneki (<code>gb_pk_live_...</code>) görünür.</p>
//         <h3>İptal (revoke)</h3>
//         <p>Bir anahtarı iptal edebilirsin; o anahtarı kullanan uygulamalar erişimini anında kaybeder.</p>
//         <h3>Bağlanma örneği</h3>
//         <pre><code><span class="tok-com">// JavaScript (fetch)</span>
// const res = await fetch(<span class="tok-str">"http://localhost:5179/api/gridbase/products"</span>, {
//   headers: { <span class="tok-key">"X-GridBase-Key"</span>: API_KEY }
// });
// const data = await res.json();</code></pre>
//         <div class="note danger">
//           <strong>Secret key'i asla istemciye koyma</strong>
//           Secret key tüm kuralları atlar. Sadece kendi sunucunda, ortam değişkeninde tut. Tarayıcıda yalnızca anon key kullan.
//         </div>
//       </section>

//       <!-- HIZLI BAŞLANGIÇ -->
//       <section id="quickstart">
//         <span class="eyebrow">5 Dakikada</span>
//         <h2>Hızlı Başlangıç</h2>
//         <h3>1. Proje seç, anahtarını al</h3>
//         <p>Console'da bir proje oluştur, API Anahtarları ekranından anon key'ini kopyala.</p>
//         <h3>2. Tablo oluştur</h3>
//         <div class="endpoint"><span class="verb post">POST</span><span class="path">/api/gridbase/tables</span></div>
//         <pre><code>{ <span class="tok-key">"name"</span>: <span class="tok-str">"todos"</span> }</code></pre>
//         <h3>3. İlk kaydı ekle</h3>
//         <p>Kolonları önceden tanımlamana gerek yok — gönderdiğin alanlar otomatik kolon olur.</p>
//         <pre><code>curl -X POST <span class="tok-str">"http://localhost:5179/api/gridbase/todos"</span> \
//   -H <span class="tok-str">"X-Project-Id: YOUR_PROJECT_ID"</span> \
//   -H <span class="tok-str">"X-GridBase-Key: gb_pk_live_xxx"</span> \
//   -H <span class="tok-str">"Content-Type: application/json"</span> \
//   -d <span class="tok-str">'{ "title": "İlk görevim", "completed": false }'</span></code></pre>
//         <h3>4. Listele</h3>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/gridbase/todos</span></div>
//         <pre><code>[
//   { <span class="tok-key">"id"</span>: <span class="tok-num">1</span>, <span class="tok-key">"title"</span>: <span class="tok-str">"İlk görevim"</span>, <span class="tok-key">"completed"</span>: <span class="tok-num">false</span> }
// ]</code></pre>
//         <p>Bu kadar. Şema, migration, sunucu kodu yok — sadece JSON gönder, JSON al.</p>
//       </section>

//       <!-- CRUD -->
//       <section id="crud">
//         <span class="eyebrow">Veri İşlemleri</span>
//         <h2>CRUD İşlemleri</h2>
//         <p>Tüm veri uçları <code>/api/gridbase/{tableName}</code> altındadır.</p>
//         <h3>Listeleme</h3>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/gridbase/{tableName}</span></div>
//         <p>Tüm kayıtları döner. Filtreleme, sıralama, arama ve sayfalama ile daraltılır (bkz. <a href="#query">Sorgulama</a>).</p>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/gridbase/{tableName}/one?filter=...</span></div>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/gridbase/{tableName}/paged?page=1&size=10</span></div>
//         <pre><code>{
//   <span class="tok-key">"page"</span>: <span class="tok-num">1</span>, <span class="tok-key">"pageSize"</span>: <span class="tok-num">10</span>, <span class="tok-key">"totalCount"</span>: <span class="tok-num">42</span>, <span class="tok-key">"totalPages"</span>: <span class="tok-num">5</span>,
//   <span class="tok-key">"data"</span>: [ <span class="tok-com">/* kayıtlar */</span> ]
// }</code></pre>
//         <h3>Tekil okuma</h3>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/gridbase/{tableName}/{id}</span></div>
//         <h3>Oluşturma</h3>
//         <div class="endpoint"><span class="verb post">POST</span><span class="path">/api/gridbase/{tableName}</span></div>
//         <p>Gövdedeki yeni alanlar otomatik kolon olur. <code>id</code> gönderme — otomatik atanır.</p>
//         <pre><code>{ <span class="tok-key">"title"</span>: <span class="tok-str">"Yeni görev"</span>, <span class="tok-key">"completed"</span>: <span class="tok-num">false</span> }</code></pre>
//         <h3>Güncelleme (tam değiştirme)</h3>
//         <div class="endpoint"><span class="verb put">PUT</span><span class="path">/api/gridbase/{tableName}/{id}</span></div>
//         <p>Kaydı gönderdiğin gövdeyle tam değiştirir. Gövdede olmayan alanlar boşaltılır (replace).</p>
//         <h3>Kısmi güncelleme</h3>
//         <div class="endpoint"><span class="verb patch">PATCH</span><span class="path">/api/gridbase/{tableName}/{id}</span></div>
//         <p>Yalnızca gönderdiğin alanları değiştirir; gerisine dokunmaz.</p>
//         <pre><code>{ <span class="tok-key">"completed"</span>: <span class="tok-num">true</span> }   <span class="tok-com">// sadece bu güncellenir</span></code></pre>
//         <table class="tbl">
//           <tr><th>Davranış</th><th>PUT</th><th>PATCH</th></tr>
//           <tr><td>Gönderilen alan</td><td>Yazılır</td><td>Yazılır</td></tr>
//           <tr><td>Gönderilmeyen alan</td><td><strong>Boşaltılır</strong></td><td><strong>Korunur</strong></td></tr>
//           <tr><td><code>null</code> gönderilen</td><td>Boşaltılır</td><td>Boşaltılır</td></tr>
//         </table>
//         <h3>Silme</h3>
//         <div class="endpoint"><span class="verb delete">DELETE</span><span class="path">/api/gridbase/{tableName}/{id}</span></div>
//         <p>Başarılıysa <code>204 No Content</code>, kayıt yoksa <code>404</code>.</p>
//       </section>

//       <!-- SORGULAMA -->
//       <section id="query">
//         <span class="eyebrow">Sorgulama</span>
//         <h2>Filtreleme & Sorgulama</h2>
//         <p>Tüm liste uçlarında geçerli sorgu parametreleri. Hepsi birleştirilebilir.</p>
//         <h3>Filtreleme</h3>
//         <p>Biçim: <code>filter=kolon:operatör:değer</code>. Birden fazla filtre <strong>VE (AND)</strong> mantığıyla çalışır. Kolon adı camelCase ya da ham ad olabilir.</p>
//         <pre><code>?filter=status:eq:published&filter=price:gt:100</code></pre>
//         <h4>Operatörler</h4>
//         <table class="tbl">
//           <tr><th>Operatör</th><th>Anlam</th><th>Örnek</th></tr>
//           <tr><td><code>eq</code> / <code>neq</code></td><td>eşittir / değildir</td><td><code>status:eq:published</code></td></tr>
//           <tr><td><code>contains</code></td><td>içerir</td><td><code>name:contains:saat</code></td></tr>
//           <tr><td><code>startswith</code> / <code>endswith</code></td><td>ile başlar / biter</td><td><code>slug:startswith:den</code></td></tr>
//           <tr><td><code>gt</code> / <code>gte</code></td><td>büyük / büyük eşit</td><td><code>price:gt:100</code></td></tr>
//           <tr><td><code>lt</code> / <code>lte</code></td><td>küçük / küçük eşit</td><td><code>stock:lt:5</code></td></tr>
//           <tr><td><code>in</code></td><td>listede (virgülle, boşluksuz)</td><td><code>status:in:published,draft</code></td></tr>
//           <tr><td><code>isnull</code> / <code>isnotnull</code></td><td>boş mu / dolu mu (değer boş)</td><td><code>couponCode:isnull:</code></td></tr>
//         </table>
//         <div class="note">
//           <strong>Tip otomatik anlaşılır</strong>
//           Sayı kolonunda sayısal, tarih kolonunda tarihsel, metin kolonunda alfabetik karşılaştırma yapılır.
//         </div>
//         <h3>Sıralama</h3>
//         <pre><code>?sort=price:desc      <span class="tok-com">// azalan</span>
// ?sort=createdAt:desc  <span class="tok-com">// en yeni önce</span></code></pre>
//         <p>Verilmezse id'ye göre artar.</p>
//         <h3>Arama</h3>
//         <pre><code>?search=laptop                       <span class="tok-com">// tüm metin kolonlarında</span>
// ?search=kırmızı&searchFields=color   <span class="tok-com">// sadece color'da</span></code></pre>
//         <p>OR mantığı (alanlardan biri eşleşsin), harf duyarsız, <code>contains</code> gibi. <code>searchFields</code> verilmezse tüm metin kolonlarında arar.</p>
//         <h3>Select</h3>
//         <pre><code>?select=name,price    <span class="tok-com">// sadece id, name, price</span>
// ?select=-description  <span class="tok-com">// description hariç</span></code></pre>
//         <p><code>id</code> her zaman döner.</p>
//         <h3>Hepsi birlikte</h3>
//         <pre><code>GET /api/gridbase/products?filter=price:gt:500&sort=price:desc
//     &select=name,price&search=pro&searchFields=name</code></pre>
//         <p>Sıra: <strong>filtre → arama → sıralama → select</strong>.</p>
//         <h3>Bağ (foreign) kolon filtreleme</h3>
//         <pre><code>?filter=gBCategoryId:eq:1         <span class="tok-com">// tekil</span>
// ?filter=gBCategoryIds:contains:2  <span class="tok-com">// çoklu (içinde arama)</span></code></pre>
//         <div class="note warn">
//           <strong>Çoklu ilişkide eq kullanma</strong>
//           Çoklu bağ kolonu <code>"1,2,3"</code> gibi tutulur; içinde arama için <code>contains</code> kullan.
//         </div>
//       </section>

//       <!-- İLİŞKİLER -->
//       <section id="relations">
//         <span class="eyebrow">İlişkisel Veri</span>
//         <h2>İlişkiler & Expand</h2>
//         <h3>İlişki kurma</h3>
//         <p>Bir tabloyu başka bir tabloya bağlarsın. İlişki kurulduğunda otomatik bir bağ kolonu oluşur: tekilde <code>GB{Tablo}Id</code>, çokluda <code>GB{Tablo}Ids</code>.</p>
//         <div class="endpoint"><span class="verb post">POST</span><span class="path">/api/gridbase/relations/{tableName}</span></div>
//         <pre><code>{ <span class="tok-key">"toTable"</span>: <span class="tok-str">"categories"</span>, <span class="tok-key">"isMultiSelect"</span>: <span class="tok-num">false</span> }</code></pre>
//         <p>Bu örnek <code>products → categories</code> ilişkisi kurar ve <code>gBCategoriesId</code> bağ kolonunu açar. Artık ürüne kategori atanabilir:</p>
//         <pre><code>POST /api/gridbase/products
// { <span class="tok-key">"title"</span>: <span class="tok-str">"Telefon"</span>, <span class="tok-key">"gBCategoriesId"</span>: <span class="tok-num">3</span> }</code></pre>
//         <p>Bir tabloyu kendine bağlarsan <code>parentId</code> kolonu oluşur — kategori ağacı, yorum yanıtları gibi hiyerarşiler için.</p>
//         <h3>Expand: ilişkili veriyi tek istekte çek</h3>
//         <p>Normalde bağ kolonu yalnızca id döner. <code>expand</code> ile ilişkili kaydın tamamını gömebilirsin.</p>
//         <pre><code>GET /api/gridbase/products?expand=categories</code></pre>
//         <pre><code>{
//   <span class="tok-key">"id"</span>: <span class="tok-num">5</span>,
//   <span class="tok-key">"title"</span>: <span class="tok-str">"Telefon"</span>,
//   <span class="tok-key">"gBCategoriesId"</span>: <span class="tok-num">3</span>,         <span class="tok-com">// id korunur</span>
//   <span class="tok-key">"categories"</span>: { <span class="tok-key">"id"</span>: <span class="tok-num">3</span>, <span class="tok-key">"name"</span>: <span class="tok-str">"Elektronik"</span> }  <span class="tok-com">// eklenir</span>
// }</code></pre>
//         <p>Birden fazlası: <code>?expand=categories,brand</code>. Çoklu ilişkide dizi döner.</p>
//         <div class="note good">
//           <strong>N+1 yok</strong>
//           Expand, ilişkili id'leri toplayıp hedef tabloyu tek sorguda çeker. 100 kaydı ilişkisiyle listelemek için 101 değil, birkaç sorgu yeterli.
//         </div>
//       </section>

//       <!-- VALIDATION -->
//       <section id="validation">
//         <span class="eyebrow">Veri Bütünlüğü <span class="pill new">Firebase'de yok</span></span>
//         <h2>Doğrulama (Validation)</h2>
//         <p>GridBase, veriyi kaydetmeden önce sunucu tarafında doğrular. Bozuk veri en baştan reddedilir — negatif fiyat, harf içeren stok, boş zorunlu alan, var olmayan ilişki.</p>
//         <h3>Kural tanımlama</h3>
//         <div class="endpoint"><span class="verb put">PUT</span><span class="path">/api/gridbase/{tableName}/columns/{columnName}/validation</span></div>
//         <pre><code>{
//   <span class="tok-key">"rules"</span>: [
//     { <span class="tok-key">"rule"</span>: <span class="tok-str">"required"</span>, <span class="tok-key">"message"</span>: <span class="tok-str">"Başlık zorunlu"</span> },
//     { <span class="tok-key">"rule"</span>: <span class="tok-str">"maxLength"</span>, <span class="tok-key">"value"</span>: <span class="tok-str">"100"</span> }
//   ]
// }</code></pre>
//         <h3>Kural tipleri</h3>
//         <table class="tbl">
//           <tr><th>Kural</th><th>Ne kontrol eder</th></tr>
//           <tr><td><code>required</code></td><td>Boş olamaz</td></tr>
//           <tr><td><code>email</code> / <code>url</code></td><td>Geçerli e-posta / URL</td></tr>
//           <tr><td><code>min</code> / <code>max</code></td><td>Sayısal sınır</td></tr>
//           <tr><td><code>minLength</code> / <code>maxLength</code> / <code>length</code></td><td>Metin uzunluğu</td></tr>
//           <tr><td><code>matches</code> / <code>pattern</code></td><td>Regex deseni</td></tr>
//           <tr><td><code>allowedValues</code></td><td>İzinli değerler listesi</td></tr>
//           <tr><td><code>integer</code> / <code>positive</code> / <code>negative</code></td><td>Tam sayı / pozitif / negatif</td></tr>
//           <tr><td><code>unique</code></td><td>Tabloda benzersiz</td></tr>
//         </table>
//         <h3>Foreign key bütünlüğü</h3>
//         <p>İlişki kolonuna var olmayan bir id atarsan reddedilir — öksüz kayıt oluşmaz.</p>
//         <pre><code>POST /api/gridbase/products
// { <span class="tok-key">"title"</span>: <span class="tok-str">"Telefon"</span>, <span class="tok-key">"gBCategoriesId"</span>: <span class="tok-num">99999</span> }

// <span class="tok-com">// → 400</span>
// { <span class="tok-key">"errors"</span>: { <span class="tok-key">"gBCategoriesId"</span>: [<span class="tok-str">"İlişkili kayıt bulunamadı (id: 99999)."</span>] } }</code></pre>
//         <h3>Hata yanıtı</h3>
//         <p>Başarısızsa <code>400</code> döner; tüm hatalar alan bazında, tek seferde listelenir.</p>
//         <pre><code>{
//   <span class="tok-key">"errors"</span>: {
//     <span class="tok-key">"title"</span>: [<span class="tok-str">"Bu alan zorunludur."</span>],
//     <span class="tok-key">"price"</span>: [<span class="tok-str">"Pozitif olmalı."</span>]
//   }
// }</code></pre>
//       </section>

//       <!-- STORAGE -->
//       <section id="storage">
//         <span class="eyebrow">Dosyalar</span>
//         <h2>Storage (Dosyalar)</h2>
//         <p>GridBase, dosya yükleme/sunma için ayrı bir Storage servisi sunar. Görseller, belgeler ve diğer dosyalar buraya yüklenir; her dosya benzersiz bir adla saklanır ve public bir URL üzerinden erişilir.</p>
//         <h3>Yükleme</h3>
//         <div class="endpoint"><span class="verb post">POST</span><span class="path">/api/File</span></div>
//         <p><code>multipart/form-data</code> ile <code>file</code> alanı gönderirsin. Kimlik doğrulama gerekir. Yanıt, dosyanın sunucudaki benzersiz adıdır (<code>guid.uzantı</code>).</p>
//         <pre><code>const form = new FormData();
// form.append(<span class="tok-str">"file"</span>, selectedFile);

// const res = await fetch(<span class="tok-str">"http://localhost:5259/api/File"</span>, {
//   method: <span class="tok-str">"POST"</span>,
//   headers: { <span class="tok-key">"X-GridBase-Key"</span>: API_KEY },
//   body: form
// });
// const fileName = await res.json();   <span class="tok-com">// "a1b2c3.png"</span></code></pre>
//         <h3>Görüntüleme / indirme</h3>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/File/{fileName}</span></div>
//         <p>Bu uç <strong>herkese açıktır</strong> (token gerektirmez) — böylece dosya URL'sini doğrudan <code>&lt;img src&gt;</code> içinde kullanabilirsin.</p>
//         <pre><code>const fileUrl = <span class="tok-str">`http://localhost:5259/api/File/${fileName}`</span>;
// <span class="tok-com">// &lt;img src={fileUrl} /&gt;</span></code></pre>
//         <h3>Listeleme</h3>
//         <div class="endpoint"><span class="verb get">GET</span><span class="path">/api/File?search=&type=&page=1&pageSize=24</span></div>
//         <p><code>type</code> ile filtrelersin: <code>image</code> ya da <code>document</code>.</p>
//         <h3>Silme</h3>
//         <div class="endpoint"><span class="verb delete">DELETE</span><span class="path">/api/File/{fileName}</span></div>
//         <p>Kimlik doğrulama gerekir. Sistem dosyaları (favicon, logo) korunur.</p>
//         <div class="note">
//           <strong>Veriyle ilişki</strong>
//           Bir kayıtta görsel tutmak istiyorsan: dosyayı Storage'a yükle, dönen dosya adını normal bir metin alanı olarak kaydına yaz (örn. <code>{ "image": "a1b2c3.png" }</code>). GridBase dosyayı değil, adını saklar; görsel URL'sini istediğin an dosya adından kurarsın.
//         </div>
//       </section>

//       <!-- ERİŞİM -->
//       <section id="access">
//         <span class="eyebrow">Yetkilendirme</span>
//         <h2>Tablo Erişim Ayarları</h2>
//         <p>Her tablo, kimin okuyup kimin yazabileceğini kendi üzerinde tutar. İstekler işlenirken bu kurallar otomatik kontrol edilir.</p>
//         <h3>AccessLevel değerleri</h3>
//         <table class="tbl">
//           <tr><th>Değer</th><th>Anlamı</th><th>Kontrol</th></tr>
//           <tr><td><code>Public</code></td><td>Herkese açık</td><td>Hiçbir kontrol yok.</td></tr>
//           <tr><td><code>Authenticated</code></td><td>Giriş yapanlar</td><td>Kullanıcının kimliği olmalı.</td></tr>
//           <tr><td><code>RoleBased</code></td><td>Belirli rol</td><td>Kullanıcının rolü gereken rolle eşleşmeli.</td></tr>
//           <tr><td><code>Owner</code></td><td>Satır sahibi</td><td>Giriş yeterli; satır filtresi ayrıca uygulanır.</td></tr>
//         </table>
//         <div class="note">
//           <strong>Admin muafiyeti</strong>
//           Admin / GB rolündeki kullanıcılar ve secret key ile gelen istekler tüm bu kontrollerden muaftır.
//         </div>
//         <h3>Erişim ayarını belirleme</h3>
//         <div class="endpoint"><span class="verb put">PUT</span><span class="path">/api/gridbase/{tableName}/access</span></div>
//         <pre><code>{
//   <span class="tok-key">"readAccess"</span>: <span class="tok-str">"Public"</span>,
//   <span class="tok-key">"writeAccess"</span>: <span class="tok-str">"RoleBased"</span>,
//   <span class="tok-key">"writeRequiredRole"</span>: <span class="tok-str">"Editor"</span>
// }</code></pre>
//         <p>Yalnızca Admin/GB çağırabilir. <code>RoleBased</code> seçilirse rol alanı boş bırakılamaz.</p>
//         <h3>Owner (satır sahipliği)</h3>
//         <p>Her kullanıcının yalnızca kendi satırlarını görmesi için kullanılır. İki katmanlıdır: API katmanında "giriş var mı" kontrol edilir; satır filtresi ise kullanıcının kimliğini <code>OwnerColumn</code>'daki değerle eşleştirir. Çalışması için tablonun <code>IsOwnerScoped = true</code> ve geçerli bir <code>OwnerColumn</code> değeri olmalıdır. Yeni satırda sahip damgası otomatik basılır.</p>
//         <h3>Hızlı referans</h3>
//         <table class="tbl">
//           <tr><th>İhtiyaç</th><th>Read</th><th>Write</th></tr>
//           <tr><td>Tamamen açık</td><td>Public</td><td>Public</td></tr>
//           <tr><td>Açık görüntüle, kısıtlı yaz</td><td>Public</td><td>RoleBased</td></tr>
//           <tr><td>Sadece giriş yapanlar</td><td>Authenticated</td><td>Authenticated</td></tr>
//           <tr><td>Role özel</td><td>RoleBased</td><td>RoleBased</td></tr>
//           <tr><td>Kişiye özel satırlar</td><td>Owner</td><td>Owner</td></tr>
//         </table>
//       </section>

//       <!-- CORS -->
//       <section id="cors">
//         <span class="eyebrow">Yönetim</span>
//         <h2>CORS Ayarları</h2>
//         <p>CORS, projenin API'sine <strong>hangi web adreslerinin tarayıcıdan erişebileceğini</strong> belirler. Proje Ayarları ekranındaki "İzinli origin'ler" bölümünden yönetilir.</p>
//         <h3>Origin ekleme</h3>
//         <p>Uygulamanın çalıştığı adresi eklersin — örn. <code>https://uygulamam.com</code>. Yalnızca eklediğin origin'lerden gelen tarayıcı istekleri kabul edilir.</p>
//         <h3>Tüm adreslere izin</h3>
//         <p><code>*</code> eklersen her adresten erişim açılır.</p>
//         <div class="note warn">
//           <strong>Üretimde * önerilmez</strong>
//           <code>*</code> projenin API'sini tüm web sitelerine açar. Geliştirme sırasında pratik olsa da, yayına alırken yalnızca kendi alan adlarını ekle.
//         </div>
//         <p>Bir origin'i kaldırdığında o adresten gelen istekler artık reddedilir.</p>
//       </section>

//       <!-- CONSOLE -->
//       <section id="console">
//         <span class="eyebrow">Görsel Arayüz</span>
//         <h2>Console Kullanımı</h2>
//         <p>Console, GridBase'in görsel yönetim panelidir — Firebase Console'a benzer, ama daha dokunulabilir. Kod yazmadan tabloları yönetir, ilişkileri kurar, veriyi düzenler ve uçları test edersin.</p>
//         <h3>Ne yapabilirsin?</h3>
//         <ul>
//           <li><strong>Tablo & kolon yönetimi:</strong> tıkla-ekle ile tablo oluştur, kolon tipi belirle, boş kolonları temizle.</li>
//           <li><strong>İlişki kurma:</strong> tablolar arası bağ tanımla (tekil/çoklu/hiyerarşi).</li>
//           <li><strong>Erişim ayarları:</strong> her tablonun okuma/yazma yetkisini görsel ayarla.</li>
//           <li><strong>Doğrulama kuralları:</strong> kolonlara required/min/max gibi kuralları arayüzden ata.</li>
//           <li><strong>Veri düzenleme:</strong> dinamik modaldan kayıt ekle, düzenle, sil.</li>
//           <li><strong>Storage:</strong> dosya yükle, görüntüle, URL kopyala.</li>
//         </ul>
//         <h3>Endpoint test ekranı</h3>
//         <p>Console'da Swagger benzeri bir test ekranı vardır: metodu seçer, parametreleri doldurur ve isteği doğrudan çalıştırırsın — kod yazmana gerek kalmaz. Create/update için hazırlanan dinamik modaldan bilgileri girer, sonucu anında görürsün.</p>
//         <h3>JSON görünümü</h3>
//         <p>Arayüzün yanında her zaman gönderdiğin/aldığın JSON görünür. Şunu görsen de:</p>
//         <pre><code>{ <span class="tok-key">"id"</span>: <span class="tok-num">5</span>, <span class="tok-key">"name"</span>: <span class="tok-str">"deneme"</span>, <span class="tok-key">"logoUrl"</span>: <span class="tok-str">"..."</span> }</code></pre>
//         <p>perde arkasında bu veri satır ve hücre olarak tutulur — ama sana hep düz JSON gösterilir. Hem kullanıcı dostu arayüz, hem de doğrudan endpoint vardır; ikisi de aynı veriye bakar.</p>
//       </section>

//       <!-- HATA KODLARI -->
//       <section id="errors">
//         <span class="eyebrow">Referans</span>
//         <h2>Hata Kodları</h2>
//         <table class="tbl">
//           <tr><th>Kod</th><th>Anlamı</th><th>Ne zaman</th></tr>
//           <tr><td><code>200</code></td><td>OK</td><td>Başarılı (okuma/güncelleme).</td></tr>
//           <tr><td><code>201</code></td><td>Created</td><td>Yeni kayıt oluştu.</td></tr>
//           <tr><td><code>204</code></td><td>No Content</td><td>Silme başarılı.</td></tr>
//           <tr><td><code>400</code></td><td>Bad Request</td><td>Doğrulama hatası / geçersiz ilişki. Gövdede <code>errors</code> döner.</td></tr>
//           <tr><td><code>403</code></td><td>Forbidden</td><td>Erişim kuralı engelledi.</td></tr>
//           <tr><td><code>404</code></td><td>Not Found</td><td>Tablo ya da kayıt bulunamadı.</td></tr>
//           <tr><td><code>409</code></td><td>Conflict</td><td>Çakışma — örn. aynı adda tablo var.</td></tr>
//         </table>
//       </section>

//       <!-- MİMARİ -->
//       <section id="architecture">
//         <span class="eyebrow">İleri Düzey</span>
//         <h2>Mimari</h2>
//         <p>Bu bölüm GridBase'i <em>kullanmak</em> için gerekli değildir; iç işleyişi merak eden ya da projeyi devralan geliştiriciler içindir.</p>
//         <h3>EAV veri modeli</h3>
//         <p>GridBase, dinamik tablolar için <strong>EAV (Entity-Attribute-Value)</strong> deseni kullanır. Sabit şemalı tablolar yerine üç çekirdek varlık vardır: <strong>Datatable</strong> (mantıksal tablo), <strong>TableRow</strong> (satır), <strong>TableCell</strong> (bir satırın bir kolondaki değeri — <strong>TableColumn</strong>'a bağlı). Veri okunurken bu yapı camelCase anahtarlı düz JSON'a dönüştürülür; yazılırken JSON tekrar hücrelere ayrılır. İlişkiler, hedef tabloyu işaret eden özel bağ kolonlarıyla (<code>GB{Tablo}Id</code>) çözülür.</p>
//         <h3>CQRS + MediatR</h3>
//         <p>Veri uçları CQRS desenine göre düzenlenmiştir. Her işlem bir Command ya da Query'ye karşılık gelir (örn. <code>CreateRowCommand</code>, <code>GetPagedRowsQuery</code>) ve ilgili Handler tarafından işlenir. Controller yalnızca isteği bir komuta çevirip MediatR üzerinden gönderir; doğrulama gibi kesişen ilgiler pipeline'da merkezî biçimde uygulanır.</p>
//         <h3>Önbellek & Domain Event</h3>
//         <p>Sık okunan veriler Redis tabanlı bir önbellekte tutulur. Geçersizleştirme domain event'lerle yürür: bir tablo değiştiğinde varlık bir olay yayar, bir <code>SaveChanges</code> interceptor'ı bunu yakalar ve ilgili önbellek anahtarlarını temizler.</p>
//         <h3>Katmanlar</h3>
//         <table class="tbl">
//           <tr><th>Katman</th><th>Sorumluluk</th></tr>
//           <tr><td>WebApi</td><td>Controller'lar, route'lar, istek/yanıt</td></tr>
//           <tr><td>Application</td><td>Command/Query/Handler (Features), iş mantığı</td></tr>
//           <tr><td>Domain</td><td>Varlıklar, domain event'ler, kurallar</td></tr>
//           <tr><td>Infrastructure</td><td>Veritabanı, repository, UnitOfWork, interceptor'lar</td></tr>
//         </table>
//         <h3>Çok kiracılılık</h3>
//         <p>Her istek bir proje (tenant) bağlamında çalışır. Bir middleware <code>X-Project-Id</code> başlığını ya da API anahtarını çözer, proje bağlamını kurar; tüm sorgular bu projeye göre süzülür. Storage ve veri servisleri ayrı veritabanlarında, ayrı kod tabanlarında çalışır.</p>
//         <div class="note">
//           <strong>Auth ayrı tutulur</strong>
//           Kimlik doğrulama uçları veri uçlarından bağımsızdır. Bu, GridBase ileride ayrı bir servise taşınırsa kendi auth'uyla birlikte taşınabilsin diyedir.
//         </div>
//       </section>

//     </div>
//   </main>
// </div>

// <script>
//   const side=document.getElementById('side'),btn=document.getElementById('menuBtn'),bd=document.getElementById('backdrop');
//   btn.addEventListener('click',()=>{side.classList.toggle('open');bd.classList.toggle('show')});
//   bd.addEventListener('click',()=>{side.classList.remove('open');bd.classList.remove('show')});
//   const links=[...document.querySelectorAll('.nav a')];
//   const map=new Map(links.map(a=>[a.getAttribute('href').slice(1),a]));
//   const obs=new IntersectionObserver((entries)=>{entries.forEach(e=>{if(e.isIntersecting){links.forEach(l=>l.classList.remove('active'));const a=map.get(e.target.id);if(a)a.classList.add('active')}})},{rootMargin:'-10% 0px -80% 0px',threshold:0});
//   document.querySelectorAll('section[id]').forEach(s=>obs.observe(s));
//   links.forEach(a=>a.addEventListener('click',()=>{side.classList.remove('open');bd.classList.remove('show')}));
//   document.querySelectorAll('pre').forEach(pre=>{const b=document.createElement('button');b.className='copy';b.textContent='Kopyala';b.addEventListener('click',()=>{const code=pre.querySelector('code')||pre;navigator.clipboard.writeText(code.innerText).then(()=>{b.textContent='Kopyalandı';setTimeout(()=>b.textContent='Kopyala',1400)})});pre.appendChild(b)});
// </script>
// </body>
// </html>









