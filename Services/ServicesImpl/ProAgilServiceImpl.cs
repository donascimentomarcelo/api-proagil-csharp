using System.Threading.Tasks;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Services.ServicesImpl
{
    public class ProAgilServiceImpl : ProAgilService
    {
        private readonly DataContext _context;
        public ProAgilServiceImpl(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public Task<Event[]> GetAllEventAsync(bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
        public Task<Event[]> GetAllEventAsyncByTheme(string theme, bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
        public Task<Event[]> GetAllSpeakersAsyncByName(bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
        public Task<Event> GetEventAsyncById(int EventId, bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
        public Task<Event> GetSpeakerAsync(int SpeakerId, bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
    }
}