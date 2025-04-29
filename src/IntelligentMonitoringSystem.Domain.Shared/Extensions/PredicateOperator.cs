using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace IntelligentMonitoringSystem.Domain.Shared.Extensions;

// Codes below are taken from https://github.com/scottksmith95/LINQKit project.

/// <summary> The Predicate Operator </summary>
public enum PredicateOperator
{
    /// <summary> The "Or" </summary>
    Or,

    /// <summary> The "And" </summary>
    And
}

/// <summary>
///     See http://www.albahari.com/expressions for information and examples.
/// </summary>
public static class PredicateBuilder
{
    /// <summary>
    ///     Creates a new ExpressionStarter with a stub expression.
    /// </summary>
    /// <param name="expr">expr</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>ExpressionStarter</returns>
    public static ExpressionStarter<T> New<T>(Expression<Func<T, bool>>? expr = null)
    {
        return new ExpressionStarter<T>(expr);
    }

    /// <summary>
    ///     Create an expression with a stub expression true or false to use when the expression is not yet started.
    /// </summary>
    /// <param name="defaultExpression">defaultExpression</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>ExpressionStarter</returns>
    public static ExpressionStarter<T> New<T>(bool defaultExpression)
    {
        return new ExpressionStarter<T>(defaultExpression);
    }

    /// <summary>
    ///     Extends the specified expression with another expression.
    /// </summary>
    /// <param name="expr1">expr1</param>
    /// <param name="expr2">expr2</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Expression</returns>
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var expr2Body = new RebindParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2Body), expr1.Parameters);
    }

    /// <summary>
    ///     Extends the specified expression with another expression.
    /// </summary>
    /// <param name="expr1">expr1</param>
    /// <param name="expr2">expr2</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>Expression</returns>
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var expr2Body = new RebindParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2Body), expr1.Parameters);
    }

    /// <summary>
    ///     Extends the specified source Predicate with another Predicate and the specified PredicateOperator.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <param name="first">The source Predicate.</param>
    /// <param name="second">The second Predicate.</param>
    /// <param name="operator">The Operator (can be "And" or "Or").</param>
    /// <returns>Expression{Func{T, bool}}</returns>
    public static Expression<Func<T, bool>> Extend<T>(
        this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second,
        PredicateOperator @operator = PredicateOperator.Or)
    {
        return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
    }

    /// <summary>
    ///     Extends the specified source Predicate with another Predicate and the specified PredicateOperator.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <param name="first">The source Predicate.</param>
    /// <param name="second">The second Predicate.</param>
    /// <param name="operator">The Operator (can be "And" or "Or").</param>
    /// <returns>Expression{Func{T, bool}}</returns>
    public static Expression<Func<T, bool>> Extend<T>(
        this ExpressionStarter<T> first,
        Expression<Func<T, bool>> second, PredicateOperator @operator = PredicateOperator.Or)
    {
        return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
    }

    /// <summary>
    ///     Rebinds the parameter of an expression to a new parameter.
    /// </summary>
    private sealed class RebindParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == oldParameter ? newParameter : base.VisitParameter(node);
        }
    }
}

/// <summary>
///     ExpressionStarter{T} which eliminates the default 1=0 or 1=1 stub expressions
/// </summary>
/// <typeparam name="T">The type</typeparam>
public class ExpressionStarter<T>
{
    private Expression<Func<T, bool>>? _predicate;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpressionStarter{T}" /> class.
    ///     Create an ExpressionStarter with a stub expression.
    /// </summary>
    public ExpressionStarter() : this(false)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpressionStarter{T}" /> class.
    /// </summary>
    /// <param name="defaultExpression">defaultExpression</param>
    public ExpressionStarter(bool defaultExpression)
    {
        if (defaultExpression)
        {
            DefaultExpression = f => true;
        }
        else
        {
            DefaultExpression = f => false;
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpressionStarter{T}" /> class.
    /// </summary>
    /// <param name="exp">exp</param>
    public ExpressionStarter(Expression<Func<T, bool>>? exp) : this(false)
    {
        _predicate = exp;
    }

    /// <summary>The actual Predicate. It can only be set by calling Start.</summary>
    private Expression<Func<T, bool>> Predicate =>
        (IsStarted || !UseDefaultExpression ? _predicate : DefaultExpression)!;

    /// <summary>Determines if the predicate is started.</summary>
    public bool IsStarted => _predicate != null;

    /// <summary> A default expression to use only when the expression is null </summary>
    public bool UseDefaultExpression => DefaultExpression != null;

    /// <summary>The default expression</summary>
    public Expression<Func<T, bool>>? DefaultExpression { get; set; }

    #region Implement Expression methods and properties

#if !NET35
    /// <summary>
    ///     CanRed
    /// </summary>
    public virtual bool CanReduce => Predicate.CanReduce;
#endif

    #endregion

    /// <summary>Set the Expression predicate</summary>
    /// <param name="exp">The first expression</param>
    /// <exception cref="Exception">Predicate cannot be started again.</exception>
    /// <returns>Expression{Func{T, bool}}</returns>
    public Expression<Func<T, bool>> Start(Expression<Func<T, bool>> exp)
    {
        if (IsStarted)
        {
#pragma warning disable S112
            throw new Exception("Predicate cannot be started again.");
#pragma warning restore S112
        }

        return _predicate = exp;
    }

    /// <summary>
    ///     Or
    /// </summary>
    /// <param name="expr2">expr2</param>
    /// <returns>Expression</returns>
    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> expr2)
    {
#pragma warning disable S1121
        return IsStarted ? _predicate = Predicate.Or(expr2) : Start(expr2);
#pragma warning restore S1121
    }

    /// <summary>
    ///     And
    /// </summary>
    /// <param name="expr2">expr2</param>
    /// <returns>Expression</returns>
    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expr2)
    {
#pragma warning disable S1121
        return IsStarted ? _predicate = Predicate.And(expr2) : Start(expr2);
#pragma warning restore S1121
    }

    /// <summary> Show predicate string </summary>
    /// <returns>string?</returns>
    public override string ToString()
    {
        return Predicate.ToString();
    }

    #region Implicit Operators

    /// <summary>
    ///     Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
    /// </summary>
    /// <param name="right">right</param>
    public static implicit operator Expression<Func<T, bool>>?(ExpressionStarter<T>? right)
    {
        return right?.Predicate;
    }

    /// <summary>
    ///     Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
    /// </summary>
    /// <param name="right">right</param>
    public static implicit operator Func<T, bool>?(ExpressionStarter<T>? right)
    {
        return right == null ? null :
            right.IsStarted || right.UseDefaultExpression ? right.Predicate.Compile() : null;
    }

    /// <summary>
    ///     Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
    /// </summary>
    /// <param name="right">right</param>
    public static implicit operator ExpressionStarter<T>?(Expression<Func<T, bool>>? right)
    {
        return right == null ? null : new ExpressionStarter<T>(right);
    }

    #endregion

    #region Implement Expression<TDelagate> methods and properties

#if !NET35

    /// <summary>Compile</summary>
    /// <returns>Func</returns>
    public Func<T, bool> Compile()
    {
        return Predicate.Compile();
    }
#endif

#if !(NET35 || WINDOWS_APP || NETSTANDARD || PORTABLE || PORTABLE40 || UAP)
    /// <summary>
    ///     Compile
    /// </summary>
    /// <param name="debugInfoGenerator">debugInfoGenerator</param>
    /// <returns>Func</returns>
    public Func<T, bool> Compile(DebugInfoGenerator debugInfoGenerator) { return Predicate.Compile(debugInfoGenerator); }

    /// <summary>
    ///     Update
    /// </summary>
    /// <param name="body">body</param>
    /// <param name="parameters">parameters</param>
    /// <returns>Expression</returns>
    public Expression<Func<T, bool>> Update(Expression body, IEnumerable<ParameterExpression> parameters)
    {
        return Predicate.Update(body, parameters);
    }
#endif

    #endregion

    #region Implement LamdaExpression methods and properties

    /// <summary>
    ///     Body
    /// </summary>
    public Expression Body => Predicate.Body;

    /// <summary>
    ///     NodeType
    /// </summary>
    public ExpressionType NodeType => Predicate.NodeType;

    /// <summary>
    ///     Parameters
    /// </summary>
    public ReadOnlyCollection<ParameterExpression> Parameters => Predicate.Parameters;

    /// <summary>
    ///     Type
    /// </summary>
    public Type Type => Predicate.Type;

#if !NET35
    /// <summary>
    ///     Name
    /// </summary>
    public string? Name => Predicate.Name;

    /// <summary>
    ///     ReturnType
    /// </summary>
    public Type ReturnType => Predicate.ReturnType;

    /// <summary>
    ///     TailCall
    /// </summary>
    public bool TailCall => Predicate.TailCall;
#endif

    #endregion
}