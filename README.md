# CityFilter-WebApi
Abat Mülakat
CITY FILTER WEB APİ
Özet bilgi ve Algoritma
Program CSV ve XML formatlarında veri alarak bu verileri istenildiği gibi filtreleyip, sıralayıp aynı format tipinde geri döndürür.
Algoritma
-> Zorunlu  verileri ve opsiyonel verileri al ve kontrol et 
	-> Zorunlu veriler varsa
		-> Gerekli dönüşüm işlemlerini yap ( Örn XML to Object)
->Opsiyonel verileri kontrol et 
	-> Filtreleme işlemleri
		-> Şehir ismine göre filtreleme
		-> İlçeye göre filtreleme
		-> Şehir koduna göre filtreleme
			-> Sıralama işlemleri
				-> Şehir ismine göre
		-> İlçeye göre filtreleme
		-> Posta koduna göre filtreleme
		-> Şehir koduna göre filtreleme	
-> Opsiyonel veriler yoksa herhangi bir değişiklik yapma
-> Zorunlu veriler yoksa hata mesajı bildir.
-> Programın çalışmasında çıkabilecek bir hata kullanıcı tabanlı olacaktır try catch ile bir hata olursa program hatayı kullanıcıya bildirecektir.
->Herhangi bir exception fırlatılmadıysa veriyi Obje formatından tekrar gönderildiği formata dönüştür ve response et.
Programın Yaptığı İşlemler
Filtreleme
Apiye gönderilen istekte kullanıcı filtrelemek istediği şeyleri çoklu olarak girebilir.
Örneğin
	Sadece şehir ismi
	Sadece ilçe ismi
	Sadece şehir kodu
	Şehir ismi ve ilçe ismi
	Şehir ismi ve şehir kodu
	İlçe ismi ve şehir kodu
Şeklinde istek göndererek istediği filtreleme işlemini yapabilmektedir.
Sıralama
Apiye gönderilen istekte kullanıcı sıralamak istediği şeyleri çoklu olarak girebilir.
Örneğin
	Sadece şehir ismine göre
	Sadece ilçe ismine göre
	Sadece posta koduna göre
	Sadece şehir koduna göre
	Hepsi
Şeklinde istek göndererek istediği sıralama işlemini yapabilmektedir.
Web Api Bilgileri
Elementler
data: Web apiye işlemesi için gönderilecek veri
Apiye gönderilen bilgi	Açıklaması
<XML> veya CSV verisi	Veri (string)

fortmatType: Web apiye gönderilen verinin formatı
Apiye gönderilen bilgi	Açıklaması
CSV	CSV formatlı veri(string)
XML	XML formatlı veri(string)

sorting: Veriye hangi sıralamanın yapılacağı bilgisi
Apiye gönderilen bilgi	Açıklaması
ASCENDING	Sıralanama tipi (string)
DESCENDING	Sıralama tipi (string)

sortingParam: Sıralamanın neye göre olacağının bilgisi
Apiye gönderilen bilgi	Açıklaması
CITY	Şehre göre sıralama (string)
CODE	Şehir koduna göre sıralama (string)
DISTRICT	İlçe ismine göre sıralama (string)
ZIPCODE	Posta koduna göre sıralama (string)
ALL	Tüm sıralamaları yap (string)
	
codefilter: Şehir koduna göre filtrelemenin bilgisi
Apiye gönderilen bilgi	Açıklaması
string	Şehir koduna göre filtreleme (string)
		Birden çok şehir  kodu filtrelenecek ise tek bir stringin içerisinde “ ,” ile ayrılmalıdır.
districtFilter: İlçeye göre filtrelemenin bilgisi
Apiye gönderilen bilgi	Açıklaması
string	İlçeye  göre filtreleme (string)
		Birden çok ilçe filtrelenecek ise tek bir stringin içerisinde “ ,” ile ayrılmalıdır.
nameFilter: Şehir ismine göre filtrelemenin bilgisi
Apiye gönderilen bilgi	Açıklaması
string	Şehir ismine göre filtreleme (string)
		Birden çok şehir filtrelenecek ise tek bir stringin içerisinde “ ,” ile ayrılmalıdır.
Fonksiyonlar
Web api de tek fonksiyon bulunmaktadır. 
Url + api/City/getCity
Metod : POST
Parametre	Veri Tipi	Zorunluluk	Açıklama
data	string	 zorunlu	 Veri
formatType	string	zorunlu	 Veri formatı
nameFilter	string	opsiyonel	Filtrelenecek şehir isimleri
districtFilter	string	opsiyonel	Filtrelenecek ilçe isimleri
codefilter	string	opsiyonel	Filtrelenecek şehir kodları
sorting	string	opsiyonel	Sıralama tipi
sortingParam	string	opsiyonel	Sıralama bilgisi

Teorik Bilgiler
Kullanılan kütüphaneler
	using System.Xml;
using System.Xml.Serialization;
using System.IO;
Farklı Veri Biçimleri
Programda yapılan işlemler objeler üzerinden yapıldığı için kolaylıkla farklı veri formatları eklenebilir. Sadece kontrol kısımlarına eklenerek veri tipinin objeye çevrilmesi objeden tekrar aynı formata çevrilmesi gerekmektedir.
Kullanılan Algoritmalar
	Kullanılan fonksiyonlarda algoritmalar açıklanmaya çalışılmıştır.
Yapım Aşamaları
	Yapım aşamaları github ile birlikte çalışılmıştır. Aşamaları commit kısımlarından bakabilirsiniz.
Açıklama
Projede kullanılan algoritmaları kendim yazmış bulunmaktayım. Bazı kısımları fazla maliyet oluşturacak iç içe for döngüleri bulunmaktadır. Bunlar kısaltılıp kod düzeltilebilir. 
Test işlemleri verilerini sistemimde herhangi bir zorluk yaşamadan çalıştırdım. Lakin veri arttıkça işlem yükü artacaktır. 
Error sınıfı ile oluşan hatalar daha düzenli kullanıcıya gösterilebilir. 
Tek bir fonksiyon olarak değil de parçalanmış işlemler olarak yapılıp birleştirilebilir yapı daha iyi olabilir. Örn api/City/getXML ile XML dönüşümleri yapılabilir. Bunun yerine fonksiyonlar tercih edildi.

