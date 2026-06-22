# BornTask1 Backend

Born Otomasyon teknik değerlendirme görevi kapsamında geliştirilmiş .NET 8 Web API projesidir.

## Kullanılan Teknolojiler

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt Password Hashing
* SMTP Mail Servisi

## Özellikler

* Kullanıcı Kayıt İşlemi
* E-Mail Doğrulama
* JWT ile Giriş İşlemi
* Şifremi Unuttum
* Şifre Sıfırlama
* Yetkilendirilmiş Endpointler
* Form Kayıt İşlemleri
* SQL Server Veritabanı Entegrasyonu
* Windows Service olarak çalışabilecek şekilde yapılandırılmıştır

## Veritabanı

Proje SQL Server veritabanı kullanmaktadır.

Migration dosyaları proje içerisinde bulunmaktadır.

Veritabanını oluşturmak için migration'lar çalıştırılmalıdır.

## Güvenlik Notu

Güvenlik nedeniyle SMTP bilgileri ve JWT Secret Key repository içerisine eklenmemiştir.

Projeyi çalıştırmadan önce `appsettings.json` dosyasındaki ilgili alanlara kendi bilgilerinizin girilmesi gerekmektedir.

## Projeyi Çalıştırma

1. Gerekli NuGet paketlerini yükleyin.
2. Migration'ları çalıştırarak veritabanını oluşturun.
3. SMTP ve JWT ayarlarını yapılandırın.
4. Projeyi çalıştırın.

Uygulama çalıştıktan sonra Swagger arayüzü üzerinden API endpointleri test edilebilir.

## Not

Uygulama standart Web API olarak çalıştırılabildiği gibi gerekli yapılandırmalar yapılarak Windows Service olarak da çalıştırılabilecek şekilde geliştirilmiştir.
