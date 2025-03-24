using Api.Controllers;
using Domain.Service;
using FakeItEasy;

namespace Testing
{
    public class TurnoverControllerTests
    {
        private readonly IBillPriceLimitService _billPriceLimitService;
        private readonly TurnoverController _turnoverController;

        public TurnoverControllerTests()
        {
            _billPriceLimitService = A.Fake<IBillPriceLimitService>();
            _turnoverController = new TurnoverController(_billPriceLimitService);
        }

        public void TurnoverController_Create_ShouldReturnOk()
        {

        }


    }
}
