using AutoMapper;
using fulbito_api.Repositories.Interfaces;


namespace fulbito_api.Services
{
	public class TournamentsService
	{
		readonly ITournamentsRepo<int> repo;
		readonly IMapper mapper;

		public TournamentsService(ITournamentsRepo<int> repo, IMapper mapper)
		{
			this.repo = repo;
			this.mapper = mapper;
		}




		void checkDates(DateTime? startDate, DateTime? endDate)
		{
			bool hasStartDate = startDate != null;
			bool hasEndDate = endDate != null;
			if (!hasStartDate && hasEndDate)
				throw new Exception("Can't set an End Date before setting a Start Date.");
			if (hasStartDate && hasEndDate && startDate > endDate)
				throw new Exception("Start Date is set after End Date.");
		}
	}
}
