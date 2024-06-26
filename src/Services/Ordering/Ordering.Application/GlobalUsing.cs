global using BuildingBlocks;
global using FluentValidation;
global using Ordering.Application.Dtos;
global using Ordering.Domain.Enums;
global using Ordering.Domain.Models;
global using Microsoft.EntityFrameworkCore;
global using Ordering.Application.Data;
global using Ordering.Domain.ValueObjects;
global using Ordering.Application.Exceptions;
global using System.Reflection;
global using BuildingBlocks.Behaviors;
global using Microsoft.Extensions.DependencyInjection;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Ordering.Domain.Events;
global using Ordering.Application.Extensions;
global using BuildingBlocks.Pagination;
global using BuildingBlocks.Messaging.Events;
global using MassTransit;
global using Ordering.Application.Orders.Commands.CreateOrder;