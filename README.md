
# RabbitMQ

**RabbitMQ**, açık kaynaklı bir mesajlaşma aracıdır ve uygulamalar arasında veri iletimini yönetir. Mesajları kuyruklarda saklayarak, bir uygulamanın mesajları göndermesine ve diğer uygulamaların bu mesajları almasına olanak tanır. Yük dengeleme, dayanıklılık ve yüksek performans gibi özellikleri sayesinde, özellikle büyük ölçekli ve asenkron veri işleme gerektiren projelerde etkili bir çözüm sunar.

![rabbitmq](https://github.com/user-attachments/assets/fa3a5e8f-9ecc-4c1e-9830-9cc50df77c93)


## Exchange Türleri

### Direct Exchange
Mesajların direkt olarak belirli bir kuyruğa gönderilmesini sağlayan exchange'dir.

![DirectExchange](https://github.com/user-attachments/assets/51342cea-19fa-40dc-9219-30f320d2b5f2)


### Fanout Exchange
Mesajların, bu exchange'e bind olmuş olan tüm kuyruklara gönderilmesini sağlar. Publisher mesajların gönderildiği kuyruk isimlerini dikkate almaz ve mesajları tüm kuyruklara gönderir.

![FanoutExchange](https://github.com/user-attachments/assets/0c0f3dde-a20e-4bd1-a22a-703e1a831684)


### Topic Exchange
Routing key'leri kullanarak mesajları kuyruklara yönlendirmek için kullanılan bir exchange'dir. Bu exchange ile routing key'in bir kısmına, keylere göre kuyruklara mesaj gönderilir. Kuyruklar da, routing key'e göre bu exchange'e abone olabilir ve sadece ilgili routing key'e göre gönderilen mesajları alabilirler.

![TopicExchange](https://github.com/user-attachments/assets/5cb0784f-8412-4272-ab49-7bec7e01501d)

**Topic formatı**
- \*, o alanda herhangi bir key olabilir. (\*.\*.fast)
- #, başında veya sonunda herhangi bir key(ler) olabilir. (#.fast)

### Headers Exchange
Routing key yerine header'ları kullanarak mesajları kuyruklara yönlendirmek için kullanılan exchange'dir.

![HeadersExchange](https://github.com/user-attachments/assets/f98d1ac6-6088-40bf-b0ef-effe6795a66a)


**x-match:** İlgili queue'nun mesajı hangi davranışla alacağının kararını veren bir key'dir. (Varsayılan olarak **any** kabul edilir.)
- **any:** İlgili queue'nun sadece tek bir key-value değerinin eşleşmesi neticesinde mesajı alacağını ifade eder.
- **all:** İlgili queue'nun tüm key-value değerlerindeki verilerin eşleşmesi neticesinde mesajı alacağını ifade eder.



## Mesaj Tasarımları

### P2P (Point-to-Point) Tasarımı
Bu tasarımda, bir publisher ilgili mesajı direkt bir kuyruğa gönderir ve bu mesaj kuyruğu işleyen bir consumer tarafından tüketilir. Eğer ki senaryo gereği bir mesajın bir tüketici tarafından işlenmesi gerekiyorsa bu yaklaşım kullanılır.

![p2p](https://github.com/user-attachments/assets/de3f545b-878b-42e7-be41-e6c5f4a7f43c)


### Publish/Subscribe (Pub/Sub) Tasarımı
Bu tasarımda publisher mesajı bir exchange'e gönderir ve böylece mesaj bu exchange'e bind edilmiş olan tüm kuyruklara yönlendirilir. Bu tasarım, bir mesajın birçok tüketici tarafından işlenmesi gerektiği durumlarda kullanışlıdır.

![pubsub](https://github.com/user-attachments/assets/1c611e42-f78a-4345-9a17-46c418e91ab1)

![pubsub2](https://github.com/user-attachments/assets/bfe95eef-56a4-42b7-86be-e83889b74801)


### Work Queue (İş Kuyruğu) Tasarımı
Bu tasarımda, publisher tarafından yayınlanmış bir mesajın birden fazla consumer arasından yalnızca biri tarafından tüketilmesi amaçlanmaktadır. Böylece mesajların işlenmesi sürecinde tüm consumer'lar aynı iş yüküne ve eşit görev dağılımına sahip olacaktırlar.
! Work Queue tasarımı, iş yükünün dağıtılması gereken ve paralel işleme ihtiyacı duyulan senaryolar için oldukça uygundur.

![work](https://github.com/user-attachments/assets/0bfdfda0-1276-4758-897a-c834658e3ef2)


### Request/Response Tasarımı
Bu tasarımda, publisher bir request yapar gibi kuyruğa mesaj gönderir ve bu mesajı tüketen consumer'dan sonuca dair başka kuyruktan bir yanıt/response bekler. Bu tarz senaryolar için oldukça uygun bir tasarımdır.

![reqres](https://github.com/user-attachments/assets/504f654e-4819-4ab6-bff2-560c63090892)
