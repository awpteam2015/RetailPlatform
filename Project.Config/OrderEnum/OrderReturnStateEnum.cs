namespace Project.Config.OrderEnum
{
    public enum OrderReturnStateEnum
    {
        待退货 = 0,
        申请退货 = 10,
        申请退款 = 20,
        确认退款 = 30,
        等待退款 = 40,
        客户退货 = 50,

        退货审核通过 = 101,
        退货审核拒绝 = 102,

        退款成功_已发货 = 201,
        退款成功_未发货 = 202,



        等待收货确认 = 40,
        收货确认 = 41

    }
}
