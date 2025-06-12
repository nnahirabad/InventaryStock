
using AutoMapper;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Features.Categorias.Commands.CreateCategorie;
using InventarioComercio.Application.Features.Categorias.Commands.DeleteCategorie;
using InventarioComercio.Application.Features.Productos.Commands.CreateProduct;
using InventarioComercio.Application.Features.Productos.Commands.DeleteProduct;
using InventarioComercio.Application.Features.Productos.Commands.UpdateProduct;
using InventarioComercio.Application.Features.Usuarios.Commands.CreateUser;
using InventarioComercio.Application.Features.Usuarios.Commands.DeleteUser;
using InventarioComercio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InventarioComercio.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // MovimientoStock 
            CreateMap<MovimientoStock, MovimientoStockDto>()
                 .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre))
                 .ReverseMap();
            

            // Producto 
            CreateMap<Producto, CreateProductCommand>().ReverseMap();
            CreateMap<Producto, UpdateProductCommand>().ReverseMap();
            CreateMap<Producto, DeleteProductCommand>().ReverseMap();

            CreateMap<Producto, ProductoDto>(); 

            // USUARIO 
            CreateMap<Usuario, RegisterCommand>();
            CreateMap<Usuario, DeleteUserCommand>();

            CreateMap<Usuario, UserDto>().ReverseMap(); 

            CreateMap<RegisterCommand, Usuario>()
           .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // O mapearlo si corresponde

            // "Cuando me den un RegisterCommand,
            // creá un nuevo Usuario, pero ignorá el campo PasswordHash
            // porque lo voy a generar manualmente".


            // CATEGORIA
            CreateMap<Categoria, CategoriaDto>()
             .ForMember(dest => dest.TotalProductos,
              opt => opt.MapFrom(src => src.Productos.Count));

            CreateMap<Categoria, CreateCategorieCommand>().ReverseMap();
            CreateMap<Categoria, DeleteCategorieCommand>().ReverseMap();
     
        }
    }
}
