namespace CustomerService.Kafka
{
    public record TransactionStatus
    (
         bool IsSucceed,
         string? Message
    );
}
