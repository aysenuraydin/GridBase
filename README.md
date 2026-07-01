<div align="center">

 🔒 **Kaynak Kod Hakkında**
 GridBase ticari bir üründür; çekirdek kaynak kodu private tutulmaktadır.
 Bu repo; mimariyi, dokümantasyonu ve canlı demoyu tanıtan bir vitrindir.
 Değerlendirme amacıyla **private repoya erişim talep edebilirsiniz** — memnuniyetle paylaşırım.

 <br/>
 🔗 **Canlı Demo:** https://www.linkedin.com/in/aysenur-aydin1  <br/>
 📧 **İletişim / Erişim talebi:** https://www.linkedin.com/in/aysenur-aydin1
</div>


<div align="center">

# 🛡️ GridBase

**BaaS Motoru & Çekirdek API**

*"Mimari özgürlük, kesintisiz performans."*

Modern web uygulamaları ve low-code platformlar için tasarlanmış, yüksek performanslı ve çok kiracılı (multi-tenant) bir **Backend-as-a-Service (BaaS)** motoru.

<br/>

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?style=flat-square&logo=postgresql&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-Cache-DC382D?style=flat-square&logo=redis&logoColor=white)
![React](https://img.shields.io/badge/Console-React%2018-61DAFB?style=flat-square&logo=react&logoColor=black)
![CQRS](https://img.shields.io/badge/Pattern-CQRS%20%2B%20MediatR-6b46c8?style=flat-square)
![License](https://img.shields.io/badge/Status-Active%20Development-orange?style=flat-square)

</div>

> 🚧 **Not:** Bu proje aktif geliştirme aşamasındadır ve **WorkGrid** ekosisteminin ana omurgası olarak inşa edilmektedir. Çekirdek özellikler (CRUD, doğrulama, ilişkiler, çok kiracılılık, yetkilendirme, Console) çalışır durumdadır; gerçek zamanlı motor ve yapay zeka modülleri yol haritasındadır.

---

## 📖 İçindekiler

- [GridBase Nedir?](#-gridbase-nedir)
- [Neden GridBase?](#️-neden-gridbase)
- [Öne Çıkan Özellikler](#-öne-çıkan-özellikler)
- [Mimari](#️-mimari)
- [Veri Modeli (EAV)](#-veri-modeli-eav)
- [Hızlı Başlangıç](#-hızlı-başlangıç)
- [Firebase ile Karşılaştırma](#-firebase-ile-karşılaştırma)
- [Geliştirme Durumu](#️-geliştirme-durumu)
- [WorkGrid Entegrasyonu](#-workgrid-entegrasyonu)

---

## 🧭 GridBase Nedir?

GridBase, bir uygulamanın arka ucunu sıfırdan yazmak yerine kullanabileceğin hazır bir **veri katmanı + REST API**'dir. Bir tablo tanımlarsın, kolonlarını belirlersin (ya da veri eklerken otomatik açılır), ve anında o tabloya veri ekleyip okuyabileceğin uçlar elde edersin — tıpkı Firebase ya da Supabase gibi.

Farkı şudur: GridBase **tek merkezi bir servis** olarak çalışır; tüm projeler ve veriler izole birer **tenant** olarak tutulur. Üstelik NoSQL tabanlı BaaS'lerin sunmadığı bir şey verir — **sunucu taraflı doğrulama**: zorunlu alanlar, sayı aralıkları, benzersizlik ve ilişki bütünlüğü en baştan garanti altına alınır.

İki şekilde kullanılır:
- 🖥️ **Görsel Console** ile — kod yazmadan tablo, kolon, ilişki ve erişim yönetimi (Firebase Console benzeri).
- 🔌 **REST API** ile — doğrudan kendi uygulamandan tüketim.

İkisi de aynı veriye bakar.

---

## 🛠️ Neden GridBase?

Geleneksel BaaS çözümleri bazen katı kalıplar sunar. GridBase, numerolojisindeki **"2" (Denge ve İş Birliği)** enerjisiyle, frontend ihtiyaçlarını (dinamik kolon yapısı, esnek tablolar, özel sıralama) backend ile kusursuz bir uyum içinde tutmak için tasarlandı.

- 🧩 **Grid-Logic** — Sürükle-bırak arayüzler için optimize edilmiş dinamik veri modelleri.
- 🏗️ **Mimari Özgürlük** — Temiz, katmanlı mimari (DDD + CQRS); her parça yerli yerinde, genişlemeye açık.
- 🔒 **Çok Kiracılı İzolasyon** — Her projenin verisi diğerlerinden tamamen ayrı; sızıntı yok.
- ✅ **Sunucu Taraflı Doğrulama** — Bozuk veri en baştan reddedilir; iş kuralları backend'de korunur.
- 🤖 **Yapay Zeka Odaklı Çekirdek** *(planlanıyor)* — NLP tabanlı şema üretimi ve akıllı veri işleme altyapısı.

---

## 🚀 Öne Çıkan Özellikler

| Özellik | Açıklama |
| --- | --- |
| **Dinamik Tablolar** | Migration yok. Veri eklerken yeni alanlar otomatik kolon olur. |
| **Tam CRUD** | `GET` / `POST` / `PUT` / `PATCH` / `DELETE` — sayfalama dahil. |
| **Gelişmiş Sorgulama** | `eq, neq, gt, gte, lt, lte, contains, startswith, endswith, in, isnull` operatörleri + sıralama + çok alanlı arama + alan seçimi (`select`). |
| **İlişkiler & Expand** | Tablolar arası bağ (tekil/çoklu/hiyerarşi); `?expand=` ile ilişkili veriyi tek istekte çek (N+1 yok). |
| **Doğrulama** | `required, min/max, minLength/maxLength, email, url, regex, unique, integer, positive` + foreign key bütünlüğü. |
| **Çok Kiracılılık** | Proje (tenant) bazlı izolasyon; `X-Project-Id` ya da API anahtarıyla otomatik çözümleme. |
| **Yetkilendirme** | Tablo bazlı erişim: `Public / Authenticated / RoleBased / Owner` + satır sahipliği (owner-scope). |
| **API Anahtarları** | `anon` (frontend, kurallara tabi) ve `secret` (backend, bypass) anahtar çiftleri. |
| **Storage** | Ayrı dosya servisi — yükleme, public görüntüleme, listeleme, silme. |
| **Önbellek** | Redis tabanlı; domain event'lerle otomatik geçersizleştirme. |
| **Görsel Console** | React tabanlı yönetim paneli; tablo/kolon/ilişki/erişim/doğrulama yönetimi + endpoint test ekranı. |

---

## 🏗️ Mimari

GridBase, **Domain-Driven Design** ve **CQRS** prensipleriyle katmanlı olarak tasarlanmıştır.

```
┌─────────────────────────────────────────────┐
│  WebApi          Controller'lar, route'lar    │
├─────────────────────────────────────────────┤
│  Application     Command / Query / Handler    │
│                  (MediatR) · iş mantığı       │
├─────────────────────────────────────────────┤
│  Domain          Entity'ler · domain event'ler│
├─────────────────────────────────────────────┤
│  Infrastructure  EF Core · Repository ·       │
│                  UnitOfWork · Interceptor'lar │
└─────────────────────────────────────────────┘
```

- **CQRS + MediatR:** Her işlem bir `Command` ya da `Query`'dir (örn. `CreateRowCommand`, `GetPagedRowsQuery`). Doğrulama gibi kesişen ilgiler pipeline davranışlarında merkezîdir.
- **Domain Event + Interceptor:** Veri değiştiğinde entity bir olay yayar; bir `SaveChanges` interceptor'ı bunu yakalayıp ilgili Redis önbelleğini temizler.
- **UnitOfWork + Repository:** Tutarlı işlem yönetimi; tek context üzerinden atomik kayıt.
- **Çok Kiracılılık:** Bir middleware her isteğin proje bağlamını (`X-Project-Id` / API key) çözer; tüm sorgular bu projeye göre süzülür.

**Servis yapısı:** Veri API'si ve dosya (Storage) API'si ayrı servisler olarak çalışır; her biri kendi veritabanına sahiptir.

---

## 🧬 Veri Modeli (EAV)

GridBase, tabloları tamamen dinamik tutmak için **EAV (Entity-Attribute-Value)** modeli kullanır. Sabit şemalı fiziksel tablolar yerine üç çekirdek varlık vardır:

```
Datatable  ─┬─  TableColumn   (kolon tanımı)
            └─  TableRow  ──  TableCell  (değer, kolona bağlı)
```

API tarafında bunu hiç fark etmezsin — her zaman düz JSON görür ve düz JSON gönderirsin:

```json
{ "id": 5, "title": "Telefon", "price": 14999, "categoryId": 3 }
```

Perde arkasında bu satır/hücre olarak tutulur, ama sana camelCase anahtarlı temiz bir nesne olarak döner. Bu sayede **şema değiştirmek için migration gerekmez** — yeni alan gönder, kolon otomatik açılsın.

---

## ⚡ Hızlı Başlangıç

**1. Tablo oluştur**
```bash
curl -X POST "http://localhost:5179/api/gridbase/tables" \
  -H "X-Project-Id: YOUR_PROJECT_ID" \
  -H "X-GridBase-Key: gb_pk_live_xxx" \
  -H "Content-Type: application/json" \
  -d '{ "name": "todos" }'
```

**2. Kayıt ekle** (kolonlar otomatik açılır)
```bash
curl -X POST "http://localhost:5179/api/gridbase/todos" \
  -H "X-Project-Id: YOUR_PROJECT_ID" \
  -H "X-GridBase-Key: gb_pk_live_xxx" \
  -H "Content-Type: application/json" \
  -d '{ "title": "İlk görevim", "completed": false }'
```

**3. Sorgula** (filtre + sıralama + sayfalama)
```bash
curl "http://localhost:5179/api/gridbase/todos/paged?page=1&size=10&filter=completed:eq:false&sort=id:desc" \
  -H "X-GridBase-Key: gb_pk_live_xxx"
```

> Şema yok, migration yok, sunucu kodu yok — sadece JSON gönder, JSON al.

---

## 🔥 Firebase ile Karşılaştırma

| | Firebase / Firestore | **GridBase** |
| --- | --- | --- |
| Veri modeli | Doküman (NoSQL) | Dinamik tablo (EAV) |
| Sunucu taraflı doğrulama | ❌ Yok (kurallar sınırlı) | ✅ Tam (required, regex, unique, FK…) |
| İlişkisel sorgu | Sınırlı | ✅ İlişki + expand (join) |
| Görsel yönetim | ✅ Console | ✅ Console + endpoint test |
| Çok kiracılılık | Proje bazlı | ✅ Proje (tenant) izolasyonu |
| Barındırma | Google bulutu | Tek merkezi servis |

---

## 🗺️ Geliştirme Durumu

**Tamamlananlar:**
- [x] **Çekirdek API & CQRS** — Command/Query/Handler mimarisi, MediatR pipeline.
- [x] **GridStore** — PostgreSQL + EF Core, dinamik şema (EAV) desteği.
- [x] **Yetkilendirme & Çok Kiracılılık** — JWT + API key (anon/secret), tenant izolasyonu, tablo bazlı erişim, owner-scope.
- [x] **Doğrulama Motoru** — Kolon bazlı kurallar + foreign key bütünlüğü.
- [x] **İlişkiler & Expand** — Tekil/çoklu/hiyerarşi ilişki, N+1 önleyen expand.
- [x] **Önbellek** — Redis + domain event tabanlı geçersizleştirme.
- [x] **Storage** — Ayrı dosya servisi (yükleme/sunma/silme).
- [x] **Görsel Console** — React yönetim paneli + endpoint test ekranı.

**Yol haritası:**
- [ ] **Real-Time Engine** — WebSocket / SignalR üzerinden anlık senkronizasyon.
- [ ] **Toplu İşlem (Bulk)** — Tek istekte çoklu satır create/update.
- [ ] **Hesaplanan Alanlar** — Formül tabanlı türetilmiş kolonlar (örn. `amount * price = total`).
- [ ] **Yapay Zeka Şema Motoru** — Doğal dil (NLP) komutlarını fiziksel tablo ve endpoint'lere çeviren motor.
- [ ] **OpenCV Görüntü İşleme Hattı** — Fatura/belge fotoğraflarını otomatik okuyup tablolara işleme.

---

## 🔗 WorkGrid Entegrasyonu

GridBase, **WorkGrid (Low-Code OS)** platformunun ihtiyaç duyduğu esnek altyapıyı sağlamak için doğdu. WorkGrid içindeki karmaşık tablo düzenleme, veriler arası özel ilişkiler ve formülasyon (hesaplamalı hücre) işlemleri gücünü tamamen bu altyapıdan alır.

---

## 📬 Takipte Kalın

İlk stabil versiyon (**v0.1.0**) için çalışmalar tüm hızıyla sürüyor. Gelişmeleri izlemek için repoyu **Star**'layabilir veya **Watch**'a alabilirsiniz.

<div align="center">

**AYŞENUR AYDIN** — *Software Architect*

</div>

---
---

<div align="center">

# 🛡️ GridBase

**The Core API & BaaS Engine**

*"Architectural freedom, relentless performance."*

A high-performance, multi-tenant **Backend-as-a-Service (BaaS)** engine designed for modern web applications and low-code platforms.

</div>

> 🚧 **Note:** This project is under active development as the backbone of the **WorkGrid** ecosystem. Core features (CRUD, validation, relations, multi-tenancy, authorization, Console) are functional; the real-time engine and AI modules are on the roadmap.

---

## 🧭 What is GridBase?

Instead of writing a backend from scratch, GridBase gives you a ready **data layer + REST API**. Define a table, set its columns (or let them auto-create as you insert data), and instantly get endpoints to read and write — just like Firebase or Supabase.

The difference: GridBase runs as a **single central service** where every project lives as an isolated **tenant**. And it offers what NoSQL-based BaaS solutions don't — **server-side validation**: required fields, numeric ranges, uniqueness, and referential integrity, all guaranteed up front.

Use it two ways: through the **visual Console** (no-code table/column/relation/access management) or directly via the **REST API**. Both read the same data.

---

## 🚀 Key Features

| Feature | Description |
| --- | --- |
| **Dynamic Tables** | No migrations. New fields auto-create as columns on insert. |
| **Full CRUD** | `GET` / `POST` / `PUT` / `PATCH` / `DELETE` with pagination. |
| **Advanced Querying** | `eq, neq, gt, gte, lt, lte, contains, startswith, endswith, in, isnull` + sorting + multi-field search + field selection. |
| **Relations & Expand** | Single/multi/self relations; `?expand=` fetches related data in one request (no N+1). |
| **Validation** | `required, min/max, length, email, url, regex, unique, integer, positive` + foreign key integrity. |
| **Multi-Tenancy** | Project-based isolation via `X-Project-Id` or API key. |
| **Authorization** | Per-table access: `Public / Authenticated / RoleBased / Owner` + row ownership. |
| **API Keys** | `anon` (frontend, rule-bound) and `secret` (backend, bypass) key pairs. |
| **Storage** | Dedicated file service — upload, public serve, list, delete. |
| **Caching** | Redis-backed, auto-invalidated via domain events. |
| **Visual Console** | React admin panel for tables, columns, relations, access, validation + endpoint tester. |

---

## 🏗️ Architecture

Built on **Domain-Driven Design** and **CQRS**:

```
WebApi          →  Controllers, routes
Application     →  Command / Query / Handler (MediatR)
Domain          →  Entities, domain events
Infrastructure  →  EF Core, Repository, UnitOfWork, Interceptors
```

- **CQRS + MediatR** — every operation is a Command or Query; cross-cutting concerns live in the pipeline.
- **Domain Events + Interceptor** — a `SaveChanges` interceptor clears the relevant Redis cache when data changes.
- **Multi-Tenancy** — middleware resolves each request's project context; all queries are scoped to it.

---

## ⚡ Quick Start

```bash
# Create a table
curl -X POST "http://localhost:5179/api/gridbase/tables" \
  -H "X-GridBase-Key: gb_pk_live_xxx" \
  -H "Content-Type: application/json" \
  -d '{ "name": "todos" }'

# Insert a record (columns auto-create)
curl -X POST "http://localhost:5179/api/gridbase/todos" \
  -H "X-GridBase-Key: gb_pk_live_xxx" \
  -H "Content-Type: application/json" \
  -d '{ "title": "My first task", "completed": false }'
```

> No schema, no migrations, no server code — just send JSON, get JSON.

---

## 🗺️ Development Status

**Completed:**
- [x] **Core API & CQRS** — Command/Query/Handler with MediatR pipeline.
- [x] **GridStore** — PostgreSQL + EF Core with dynamic (EAV) schema.
- [x] **Auth & Multi-Tenancy** — JWT + API keys, tenant isolation, per-table access, owner-scope.
- [x] **Validation Engine** — Column rules + foreign key integrity.
- [x] **Relations & Expand** — Single/multi/self relations, N+1-free expand.
- [x] **Caching** — Redis + domain-event invalidation.
- [x] **Storage** — Dedicated file service.
- [x] **Visual Console** — React admin panel + endpoint tester.

**Roadmap:**
- [ ] **Real-Time Engine** — WebSocket / SignalR sync.
- [ ] **Bulk Operations** — Multi-row create/update in one request.
- [ ] **Computed Fields** — Formula-driven derived columns.
- [ ] **AI Schema Processor** — NLP → physical schema & endpoints.
- [ ] **OpenCV Pipeline** — Auto-parse invoices/documents into records.

---

## 🔗 The WorkGrid Ecosystem

GridBase was born to provide the flexible backend infrastructure required by **WorkGrid (Low-Code OS)**. The complex table manipulations, computational matrices, and data relations in WorkGrid are 100% powered by the GridBase engine under the hood.

---

## 📬 Stay Updated

Development is moving rapidly toward the first stable release (**v0.1.0**). **Star** or **Watch** the repository to follow our progress.

<div align="center">

**AYŞENUR AYDIN** — *Software Architect*

</div>


