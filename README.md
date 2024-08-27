
# RabbitMQ

**RabbitMQ**, açık kaynaklı bir mesajlaşma aracıdır ve uygulamalar arasında veri iletimini yönetir. Mesajları kuyruklarda saklayarak, bir uygulamanın mesajları göndermesine ve diğer uygulamaların bu mesajları almasına olanak tanır. Yük dengeleme, dayanıklılık ve yüksek performans gibi özellikleri sayesinde, özellikle büyük ölçekli ve asenkron veri işleme gerektiren projelerde etkili bir çözüm sunar.


## Exchange Türleri

### Direct Exchange
Mesajların direkt olarak belirli bir kuyruğa gönderilmesini sağlayan exchange'dir.

### Fanout Exchange
Mesajların, bu exchange'e bind olmuş olan tüm kuyruklara gönderilmesini sağlar. Publisher mesajların gönderildiği kuyruk isimlerini dikkate almaz ve mesajları tüm kuyruklara gönderir.

### Topic Exchange
Routing key'leri kullanarak mesajları kuyruklara yönlendirmek için kullanılan bir exchange'dir. Bu exchange ile routing key'in bir kısmına, keylere göre kuyruklara mesaj gönderilir. Kuyruklar da, routing key'e göre bu exchange'e abone olabilir ve sadece ilgili routing key'e göre gönderilen mesajları alabilirler.

**Topic formatı**
- \*, o alanda herhangi bir key olabilir. (\*.\*.fast)
- #, başında veya sonunda herhangi bir key(ler) olabilir. (#.fast)

### Headers Exchange
Routing key yerine header'ları kullanarak mesajları kuyruklara yönlendirmek için kullanılan exchange'dir.

**x-match:** İlgili queue'nun mesajı hangi davranışla alacağının kararını veren bir key'dir. (Varsayılan olarak **any** kabul edilir.)
- **any:** İlgili queue'nun sadece tek bir key-value değerinin eşleşmesi neticesinde mesajı alacağını ifade eder.
- **all:** İlgili queue'nun tüm key-value değerlerindeki verilerin eşleşmesi neticesinde mesajı alacağını ifade eder.



## Mesaj Tasarımları

### P2P (Point-to-Point) Tasarımı
Bu tasarımda, bir publisher ilgili mesajı direkt bir kuyruğa gönderir ve bu mesaj kuyruğu işleyen bir consumer tarafından tüketilir. Eğer ki senaryo gereği bir mesajın bir tüketici tarafından işlenmesi gerekiyorsa bu yaklaşım kullanılır.

### Publish/Subscribe (Pub/Sub) Tasarımı
Bu tasarımda publisher mesajı bir exchange'e gönderir ve böylece mesaj bu exchange'e bind edilmiş olan tüm kuyruklara yönlendirilir. Bu tasarım, bir mesajın birçok tüketici tarafından işlenmesi gerektiği durumlarda kullanışlıdır.

### Work Queue (İş Kuyruğu) Tasarımı
Bu tasarımda, publisher tarafından yayınlanmış bir mesajın birden fazla consumer arasından yalnızca biri tarafından tüketilmesi amaçlanmaktadır. Böylece mesajların işlenmesi sürecinde tüm consumer'lar aynı iş yüküne ve eşit görev dağılımına sahip olacaktırlar.
! Work Queue tasarımı, iş yükünün dağıtılması gereken ve paralel işleme ihtiyacı duyulan senaryolar için oldukça uygundur.

### Request/Response Tasarımı
Bu tasarımda, publisher bir request yapar gibi kuyruğa mesaj gönderir ve bu mesajı tüketen consumer'dan sonuca dair başka kuyruktan bir yanıt/response bekler. Bu tarz senaryolar için oldukça uygun bir tasarımdır.