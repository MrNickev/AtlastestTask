using AtlasTestTask.Models;

namespace AtlasTestTask.Services;

public interface IPaymentScheduleService
{
    public PaymentScheduleModel CreateScheduleByCredit(CreditModel credit);
    public PaymentScheduleModel CreateScheduleByCredit(CreditDailyModel credit);
}