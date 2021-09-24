
using FluentValidation;
using Tasks.Application.Features.HotelFeatures.Queries;

namespace Tasks.Validators
{
    public class GetHotelDetailsByParamsQueryValidator : AbstractValidator<GetHotelDetailsByParamsQuery>
    {
        public GetHotelDetailsByParamsQueryValidator()
        {
            RuleFor(c => c.arrivalDate).NotNull();
            RuleFor(c => c.hotelID)
                .NotNull()
                .GreaterThan(0);
            RuleFor(c => c.jsonDataContxt).Must(item => item.Count > 0);
        }
    }
}
