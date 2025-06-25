namespace AdInsights.ServiceLibrary.Interface;

public interface ITenantContextService
{
    string TenantId { get; }
    void SetTenantId(string tenantId);
}
