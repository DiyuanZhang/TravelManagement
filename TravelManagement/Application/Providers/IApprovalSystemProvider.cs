using System.Threading.Tasks;
using TravelManagement.Application.Dtos;

namespace TravelManagement.Application.Providers
{
	public interface IApprovalSystemProvider
	{
		Task Approve(long userId, ApproveRequest request);
	}
}