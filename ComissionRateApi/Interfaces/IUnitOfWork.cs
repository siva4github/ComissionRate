namespace ComissionRateApi.Interfaces;

public interface IUnitOfWork
{
    ICompanyRepo CompanyRepo {get;}
    ICustomerRepo CustomerRepo {get;}
    IDistributionRepo DistributionRepo {get;}
    IOrderRepo OrderRepo {get;}
    IProductRepo ProductRepo {get;}

    bool HasChanges();
    Task<bool> CompleteAsync();
    void Update<TEntity>(TEntity entity);
}