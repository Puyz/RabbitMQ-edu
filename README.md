
# RabbitMQ

**RabbitMQ**, açık kaynaklı bir mesajlaşma aracıdır ve uygulamalar arasında veri iletimini yönetir. Mesajları kuyruklarda saklayarak, bir uygulamanın mesajları göndermesine ve diğer uygulamaların bu mesajları almasına olanak tanır. Yük dengeleme, dayanıklılık ve yüksek performans gibi özellikleri sayesinde, özellikle büyük ölçekli ve asenkron veri işleme gerektiren projelerde etkili bir çözüm sunar.


## Exchange Türleri

### Direct Exchange
- Mesajların direkt olarak belirli bir kuyruğa gönderilmesini sağlayan exchange'dir.

### Fanout Exchange
- Mesajların, bu exchange'e bind olmuş olan tüm kuyruklara gönderilmesini sağlar. Publisher mesajların gönderildiği kuyruk isimlerini dikkate almaz ve mesajları tüm kuyruklara gönderir.

### Topic Exchange
- Routing key'leri kullanarak mesajları kuyruklara yönlendirmek için kullanılan bir exchange'dir. Bu exchange ile routing key'in bir kısmına, keylere göre kuyruklara mesaj gönderilir. Kuyruklar da, routing key'e göre bu exchange'e abone olabilir ve sadece ilgili routing key'e göre gönderilen mesajları alabilirler.

**Topic formatı**
- \*, o alanda herhangi bir şey olabilir. (\*.\*.fast)
- #, başında veya sonunda herhangi bişiler olabilir. (#.fast)