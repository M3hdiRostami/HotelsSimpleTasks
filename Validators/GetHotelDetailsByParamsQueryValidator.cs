
using FluentValidation;
using Tasks.Application.Features.HotelFeatures.Queries;

namespace Tasks.Validators
{
    public class GetHotelDetailsByParamsQueryValidator : AbstractValidator<GetHotelDetailsByParamsQuery>
    {
        public GetHotelDetailsByParamsQueryValidator()
        {
            RuleFor(c => c.ArrivalDate).NotNull();
            RuleFor(c => c.HotelID)
                .NotNull()
                .GreaterThan(0);
            RuleFor(c => c.JsonDataContxt).Must(item => item.Count > 0);
        }
    }
}
