using AutoMapper;
using AMS.Api.Models;
using AMS.Api.Dtos;
namespace AMS.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetResponseDto>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            .ForMember(dest => dest.AssetTypeName, opt => opt.MapFrom(src => src.AssetType.Name))
            .ForMember(x => x.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice.Number));
            CreateMap<AssetCreateDto, Asset>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Location, LocationResponseDto>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<Supplier, SupplierResponseDto>();
            CreateMap<SupplierCreateDto, Supplier>();
            CreateMap<Invoice, InvoiceResponseDto>();
            CreateMap<InvoiceCreateDto, Invoice>();
            CreateMap<Maintainer, MaintainerResponseDto>()
            .ForMember(dest => dest.MaintainerTypeName, opt => opt.MapFrom(src => src.MaintainerType.Name));
            CreateMap<MaintainerCreateDto, Maintainer>();
            CreateMap<AssetType, AssetTypeResponseDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<AssetTypeCreateDto, AssetType>();
            CreateMap<AssetOwnerShip, AssetOwnerShipResponseDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset != null ? src.Asset.Name : string.Empty))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.Name : string.Empty));
            CreateMap<AssetOwnerShipCreateDto, AssetOwnerShip>();
            CreateMap<AssetStatus, AssetStatusResponseDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.Name));
            CreateMap<AssetStatusCreateDto, AssetStatus>();
            CreateMap<AssetStatusHistory, AssetStatusHistoryResponseDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.Name));
            CreateMap<AssetStatusHistoryCreateDto, AssetStatusHistory>();
            CreateMap<MaintenacePart, MaintenacePartResponseDto>()
                .ForMember(dest => dest.MaintenanceRecordId, opt => opt.MapFrom(src => src.MaintenaceRequestPartId));
            CreateMap<MaintenacePartCreateDto, MaintenacePart>()
                .ForMember(dest => dest.MaintenaceRequestPartId, opt => opt.MapFrom(src => src.MaintenanceRecordId))
                .ForMember(dest => dest.MaintenanceRecord, opt => opt.Ignore()); // Will be set manually
            CreateMap<MaintenanceRecord, MaintenanceRecordResponseDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.Name))
            .ForMember(dest => dest.MaintainerName, opt => opt.MapFrom(src => src.Maintainer.Name));
            CreateMap<MaintenanceRecordCreateDto, MaintenanceRecord>();
            CreateMap<OwnerType, OwnerTypeResponseDto>();
            CreateMap<OwnerTypeCreateDto, OwnerType>();
            CreateMap<Owner, OwnerResponseDto>()
            .ForMember(dest => dest.OwnerTypeName, opt => opt.MapFrom(src => src.OwnerType.Name));
            CreateMap<OwnerCreateDto, Owner>();
            CreateMap<MaintainerType, MaintainerTypeResponseDto>();
            CreateMap<MaintainerTypeCreateDto, MaintainerType>();
            CreateMap<TemporaryUser, TemporaryUserResponseDto>();
            CreateMap<TemporaryUserCreateDto, TemporaryUser>();
            CreateMap<TemporaryUsedRecord, TemporaryUsedRecordResponseDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset != null ? src.Asset.Name : string.Empty))
            .ForMember(dest => dest.TemporaryUserName, opt => opt.MapFrom(src => src.TemporaryUser != null ? src.TemporaryUser.Name : string.Empty));
            CreateMap<TemporaryUsedRecordCreateDto, TemporaryUsedRecord>();
            CreateMap<TemporaryUsedRequest, TemporaryUsedRequestResponseDto>()
            .ForMember(dest => dest.TemporaryUsedRecordName, opt => opt.MapFrom(src => src.TemporaryUsedRecord.Name));
            CreateMap<TemporaryUsedRequestCreateDto, TemporaryUsedRequest>();
        }
    }
}