using AtlasTestTask.Models;

namespace AtlasTestTask.Services;

public class PaymentScheduleService : IPaymentScheduleService
{
    public PaymentScheduleModel CreateScheduleByCredit(CreditModel credit)
    {
        var value = credit.Value;
        var term = credit.Term;
        var rate = credit.Rate / 100;
        var monthPayment = Math.Round(rate/12 * Math.Pow(rate / 12 + 1, term)/(Math.Pow(rate / 12 + 1, term) - 1) * value, 2);

        var shedule = new PaymentScheduleModel();
        for (var i = 0; i < term; i++)
        {
            var payment = new Payment();
            payment.Date = (i == 0 ? DateTime.Now : shedule.Payments[i-1].Date).AddMonths(1);
            payment.Value = (float)monthPayment;
            payment.RatePayment = (float)Math.Round((i == 0 ? value : shedule.Payments[i - 1].ReaminingDebt) * rate / 12, 2);
            payment.BasePayment = (float)Math.Round(monthPayment - payment.RatePayment, 2);
            payment.ReaminingDebt =(float)Math.Round(
                i == 0 ? value - payment.BasePayment : shedule.Payments[i - 1].ReaminingDebt - payment.BasePayment, 2);
            if (payment.ReaminingDebt < 0)
                payment.ReaminingDebt = 0;
            shedule.Payments.Add(payment);
        }

        shedule.OverrideAmount = (float)(monthPayment * term);
        return shedule;
    }

    public PaymentScheduleModel CreateScheduleByCredit(CreditDailyModel credit)
    {
        var rate = credit.Rate / 100 * credit.Step;
        var value = credit.Value;
        int periodCount = credit.Term % credit.Step == 0 ? credit.Term / credit.Step : credit.Term / credit.Step + 1;
        var periodPayment = Math.Round(value * rate *(Math.Pow(1 + rate, periodCount) / (Math.Pow(1 + rate, periodCount) - 1)), 2);
        var schedule = new PaymentScheduleModel();
        for (var i = 0; i < periodCount; i++)
        {
            var payment = new Payment();
            payment.Date = (i == 0 ? DateTime.Now : schedule.Payments[i-1].Date).AddDays(credit.Step);
            payment.Value = (float)periodPayment;
            payment.RatePayment = (float)Math.Round((i == 0 ? value : schedule.Payments[i - 1].ReaminingDebt) * rate, 2);
            payment.BasePayment = (float)Math.Round(periodPayment - payment.RatePayment, 2);
            payment.ReaminingDebt =(float)Math.Round(
                i == 0 ? value - payment.BasePayment : schedule.Payments[i - 1].ReaminingDebt - payment.BasePayment, 2);
            if (payment.ReaminingDebt < 0)
                payment.ReaminingDebt = 0;
            schedule.Payments.Add(payment);
        }

        schedule.OverrideAmount = (float)(periodPayment * periodCount);
        return schedule;

    }
}