using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Supplier, string?>> selector, string? value, int idIgnor)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        // cria um novo parâmetro para compor a expressão final
        var parameter = Expression.Parameter(typeof(Supplier), "p");
        var replacer = new ParameterReplacer(selector.Parameters[0], parameter);
        var bodyWithNewParam = replacer.Visit(selector.Body)!;

        // p.Id != idIgnor
        var idProperty = Expression.Property(parameter, "Id");
        var idNotEqual = Expression.NotEqual(idProperty, Expression.Constant(idIgnor));

        // selector(p) == value
        var valueExpression = Expression.Constant(value, typeof(string));
        var equalsExpression = Expression.Equal(bodyWithNewParam, valueExpression);

        // p => p.Id != idIgnor && selector(p) == value
        var combined = Expression.AndAlso(idNotEqual, equalsExpression);
        var lambda = Expression.Lambda<Func<Supplier, bool>>(combined, parameter);

        return await _context.Set<Supplier>().AnyAsync(lambda);
    }

    // Helper para substituir parâmetro nas expressões
    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly ParameterExpression _to;

        public ParameterReplacer(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node) =>
            node == _from ? _to : base.VisitParameter(node);
    }
}
