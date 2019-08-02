using System.Threading.Tasks;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Services
{
    public interface ProAgilService
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();
         Task<Event[]> GetAllEventAsyncByTheme(string theme, bool includeSpeakers);
         Task<Event[]> GetAllEventAsync(bool includeSpeakers);
         Task<Event> GetEventAsyncById(int EventId, bool includeSpeakers);
         Task<Event[]> GetAllSpeakersAsyncByName(bool includeSpeakers);
         Task<Event> GetSpeakerAsync(int SpeakerId, bool includeSpeakers);
    }
}