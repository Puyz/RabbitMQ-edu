
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
