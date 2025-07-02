public class DistributorService : IDistributorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DistributorService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DistributorResponseDTO>> GetAllDistributorsAsync()
    {
        var distributors = await _context.Distributors
            .Include(d => d.Country)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DistributorResponseDTO>>(distributors);
    }
}