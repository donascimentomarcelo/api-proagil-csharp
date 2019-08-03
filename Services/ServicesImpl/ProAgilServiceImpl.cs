using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Event[]> GetAllEventAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(c => c.Allotments)
                .Include(c => c.SocialNetworks);

                if (includeSpeakers) {
                    query = query
                    .Include(pe => pe.SpeakerEvents)
                    .ThenInclude(p => p.Speaker)
                }

                query = query.OrderByDescending(c => c.EventDate);

                return await query.ToArrayAsync();
        }
        public async Task<Event[]> GetAllEventAsyncByTheme(string theme, bool includeSpeakers)
        {
            IQueryable<Event> query = _context.Events
                .Include(c => c.Allotments)
                .Include(c => c.SocialNetworks);

                if (includeSpeakers) {
                    query = query
                    .Include(pe => pe.SpeakerEvents)
                    .ThenInclude(p => p.Speaker)
                }

                query = query.OrderByDescending(c => c.EventDate)
                             .Where(c => c.Theme.Contains(theme));

                return await query.ToArrayAsync();
        }
        public Task<Event[]> GetAllSpeakersAsyncByName(bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
        public async Task<Event> GetEventAsyncById(int EventId, bool includeSpeakers)
        {
            IQueryable<Event> query = _context.Events
                .Include(c => c.Allotments)
                .Include(c => c.SocialNetworks);

                if (includeSpeakers) {
                    query = query
                    .Include(pe => pe.SpeakerEvents)
                    .ThenInclude(p => p.Speaker)
                }

                query = query.OrderByDescending(c => c.EventDate)
                             .Where(c => c.Id == EventId);

                return await query.FirstOrDefaultAsync();
        }
        public Task<Event> GetSpeakerAsync(int SpeakerId, bool includeSpeakers)
        {
            throw new System.NotImplementedException();
        }
    }
}