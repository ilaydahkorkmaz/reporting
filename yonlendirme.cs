 //ÇAĞRI YÖNLENDİRME KODLARI

 public class CallRoutingService
 {
     private redonly CallCenterDbContext_context;

     public CallRoutingService(CallCenterDbContext context)
     {
         CallCenterDbContext_context = context;
     }

     public async Task<string> RouteCallAsync(CallRequest request)
     {
         var availableAgents = await CallCenterDbContext_context.Agents
             .Where(await async => await.IsAvailable && a.SkillSet.Contains(request.CallType)) //mesguliyeti az olana yönlendirir.
             .OrderBy(a => a.CurrentLoad)//mesguliyeti en az olana yönlendirir.
             .FirstOrderDefaultAsync();

         if (availableAgents == null)
         {
             return "Müsait Temsilci yok"
         }

         //Yönlendirme Kodları.

         availableAgents.CurrentLoad++;
         await _context.SaveChangesAsync();

         return $"Call routed to agent: {availableAgents.Name}";
     }
 }
     //ÇAĞRI YÖNLENDİRME KODLARI BİTİŞ.