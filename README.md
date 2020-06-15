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


