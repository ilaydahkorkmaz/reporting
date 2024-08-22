        //ÇAĞRI ANALİZİ VE RAPORLAMA 


        //çağrı analizini yapacak metod
        [HttpPost("ReportCallMetrics")]
        public async Task<IActionResult> ReportCallMetrics(CallMetrics metrics)

        {
            //Verilerin geçerliliğinin kontrol edilmesini sağlayacak kod

            if (metrics == null || metrics.CallDuration <= 0 || metrics.WaitTime < 0)

            {
                return BadRequest(new { message = "Geçersiz Veriler" });

            }

            try

            {
                //Veri tabanı örnek kod

                using (var context = new CallMetricsDbContext())
                {
                    //Yeni metrik verisini oluşturma kodu
                    var newMetric = new CallMetric
                    {
                        CallDuration = new CallMetric
                        {
                            CallDuration = metrics.WaitTime,
                            ReporttedAt = DateTime.UtcNow
                        };

                        //Deneme veritabanı kaydetme kodu
                        context.CallMetrics.Add(newMetric);
                    }
                }
                return Ok(new { message = "Veriler kaydedildi" });
            }
            catch (Exception ex)
            {
                //Hatanın yönetilmesi 
                return StatusCode(500, new { message = "Hata oluştu", error = ex.Message });

            }

        }
               //Çağrı Metriklerini temsil eden sınıf CallMtrics
               public class CallMetrics
{
    public int CallDuration { get; set; }//çağrı süresini, saniye
    public int WaitTime { get; set; } //bekleme süresi, saniye 
}
//veritabanı için kullanılan DbContext sınıfı

public class CallMetricsDbContext : DbContext
{
    protected ovveride void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //veritabanı bağlantı bilgisi (sql örneğidir)
        optionsBuilder.UseSqlServer("Bağlantı Dizininiz Burada");

    }

}
//Veritabanında saklanan çağrı metriklerini temsil eden sınıf

public class CallMetric
{
    public int Id { get; set; }
    public int CallDuration { get; set; }
    public int WaitTime { get; set; }
    public DateTime ReportedAt { get; set; } //verilerin raporlandığı zaman
}
           // ÇAĞRI ANALİZİ VE RAPORLAMA SONNN