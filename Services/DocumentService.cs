
using System.Collections.Generic;
using System.Threading.Tasks;
using DHL_Document_App.Data;
using DHL_Document_App.Models;
using Microsoft.EntityFrameworkCore;

namespace DHL_Document_App.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task AddDocumentAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
