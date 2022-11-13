﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            switch (node.Method.Name)
            {
                case "Equals":
                    CreateQuery(node.Object, node.Arguments[0], "(", ")");
                    return node;
                case "StartsWith":
                    CreateQuery(node.Object, node.Arguments[0], "(", "*)");
                    return node;
                case "EndsWith":
                    CreateQuery(node.Object, node.Arguments[0], "(*", ")");
                    return node;
                case "Contains":
                    CreateQuery(node.Object, node.Arguments[0], "(*", "*)");
                    return node;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
                        CreateQuery(node.Right, node.Left, "(", ")"); 
                    else if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
                        CreateQuery(node.Left, node.Right, "(", ")");
                    else
                        throw new NotSupportedException($"Expression '{node}' is not supported");
                    break;

                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    _resultStringBuilder.Append(" AND ");
                    Visit(node.Right);
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        private void CreateQuery(Expression left, Expression right, string expressionStart, string expressionEnd)
        {
            Visit(left);
            _resultStringBuilder.Append(expressionStart);
            Visit(right);
            _resultStringBuilder.Append(expressionEnd);
        }

        #endregion
    }
}
