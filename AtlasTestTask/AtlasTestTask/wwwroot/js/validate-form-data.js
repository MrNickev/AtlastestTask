$.validator.addMethod("lessThan",
    function (value, element, param) {
            var $otherElement = $(param);
            return parseInt(value, 10) <= parseInt($otherElement.val(), 10);
    });

$(document).ready(function() {
        
        $("#creditForm").validate({
                messages: {

                        Value: {
                                required: "Пожалуйста, введите сумму кредита",
                                min: "Сумма кредита не может быть меньше 1"
                        },
                        Term: {
                                required: "Пожалуйста, введите срок кредита",
                                min: "Срок кредита не может быть меньше 1 месяца"
                        },

                        Rate: {
                                required: "Пожалуйста, введите процентую ставку",
                                min: "Процентная ставка по кредиту не может быть меньше 1%"
                        },
                        Step: {
                                required: "Пожалуйста, введите шаг платежа",
                                min: "Минимальный шаг платежа - 1 день",
                                lessThan: "Шаг платежа не может быть больше срока"
                        }

                },
                rules: {
                        Value: {
                                required: true,
                                min: 1
                        },
                        Term: {
                                required: true,
                                min: 1
                        },
                        Rate: {
                                required: true,
                                min: 0
                        },
                        Step: {
                                required: true,
                                min: 1,
                                lessThan: "#term"
                                
                        }
                        
                },

        });
});