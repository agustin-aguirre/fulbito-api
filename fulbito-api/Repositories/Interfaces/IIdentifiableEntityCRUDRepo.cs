namespace fulbito_api.Repositories.Interfaces
{
	public interface IIdentifiableEntityCRUDRepo<TId, TEntity>
	{
		Task<TEntity> Create(TEntity newEntity);
		Task<bool> Delete(TId entityId);
		Task<bool> Exists(TId entityId);
		Task<TEntity?> Get(TId entityId);
		Task<IEnumerable<TEntity>> GetMany(IEnumerable<TId> entityIds);
		Task Update(TEntity updatedEntity);
	}
}