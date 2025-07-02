using AutoMapper;
using BakeryAPI.Data;
using BakeryAPI.DTOs;
using BakeryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BakeryAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeedbackService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FeedbackResponseDTO> CreateFeedbackAsync(FeedbackCreateDTO dto, int customerId)
        {
            var feedback = _mapper.Map<Feedback>(dto);
            feedback.CustomerId = customerId;

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return await GetFeedbackByIdAsync(feedback.Id);
        }

        public async Task DeleteFeedbackAsync(int id, int customerId)
        {
            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.Id == id && f.CustomerId == customerId);

            if (feedback == null)
                throw new KeyNotFoundException("Feedback not found or you don't have permission");

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<FeedbackResponseDTO> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (feedback == null)
                return null;

            return _mapper.Map<FeedbackResponseDTO>(feedback);
        }
    }
}