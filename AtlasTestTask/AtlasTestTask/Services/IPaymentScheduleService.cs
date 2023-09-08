using AtlasTestTask.Models;

namespace AtlasTestTask.Services;

public interface IPaymentScheduleService
{
    PaymentScheduleModel CreateScheduleByCredit(CreditModel credit);
    PaymentScheduleModel CreateScheduleByCredit(CreditDailyModel credit);
}