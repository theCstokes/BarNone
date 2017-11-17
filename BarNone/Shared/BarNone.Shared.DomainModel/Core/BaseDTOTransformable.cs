using BarNone.Shared.DataTransfer.Core;
using System;

namespace BarNone.Shared.DTOTransformable.Core
{
    public abstract class DTOTransformable<TDTOTransferObject, TDTO> : IDTOTransformable<TDTO>
        where TDTOTransferObject : IDTOTransformable<TDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
    {
        
        dynamic IDTOTransformable.CreateDTO(ConvertConfig config = null)
        {
            return CreateDTO(config);
        }

        public virtual TDTO CreateDTO(ConvertConfig config = null)
        {
            if (!config.CanContinue) return null;
            var dto = OnBuildDTO();
            return dto;
        }

        public virtual void PopulateFromDTO(TDTO dto, ConvertConfig config = null)
        {
            OnPopulate(dto);
        }

        public static TDTOTransferObject CreateFromDTO(TDTO dto, ConvertConfig config = null)
        {
            var dm = new TDTOTransferObject();
            dm.PopulateFromDTO(dto);
            return dm;
        }

        protected abstract TDTO OnBuildDTO();

        protected abstract void OnPopulate(TDTO dto, ConvertConfig config = null);
    }

    public abstract class DetailDTOTransformable<TDTOTransferObject, TDTO, TDetailDTO> : DTOTransformable<TDTOTransferObject, TDTO>
        where TDTOTransferObject : DetailDTOTransformable<TDTOTransferObject, TDTO, TDetailDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        public override TDTO CreateDTO(ConvertConfig config = null)
        {
            if (!config.CanContinue) return null;
            var dto = OnBuildDTO();

            var detailConfig = config.GetNext();

            dto.Details = OnBuildDetailDTO(detailConfig);
            return dto;
        }

        public override void PopulateFromDTO(TDTO dto, ConvertConfig config = null)
        {

            if (config != null)
            {
                if (!config.CanContinue) return;
                config.Parent = GetParent();
            }
            base.PopulateFromDTO(dto, config);
        }

        protected abstract TDetailDTO OnBuildDetailDTO(ConvertConfig config);

        public virtual dynamic GetParent()
        {
            return null;
        }

        public virtual void SetParent(dynamic parent)
        {

        }

        
    }

    public class ConvertConfig
    {
        #region Public Constructor(s).
        public ConvertConfig(dynamic parent = null)
        {
            Infinite = true;
            Depth = Int32.MaxValue;
            Parent = parent;
        }

        public ConvertConfig(int? depth, dynamic parent = null)
        {
            Infinite = !depth.HasValue;
            Depth = depth ?? Int32.MaxValue;
            Parent = parent;
        }
        #endregion

        #region Public Property(s).
        public dynamic Parent { get; set; }

        public bool CanContinue
        {
            get
            {
                if (Infinite) return true;
                return (Depth > 0);
            }
        }

        public bool Infinite { get; set; } 
        #endregion

        #region Private Property(s).
        private int Depth { get; set; }
        #endregion

        #region Public Member(s).
        public ConvertConfig GetNext(dynamic parent = null)
        {
            if (parent == null) parent = Parent;

            if (Infinite)
            {
                return new ConvertConfig(parent);
            }
            else
            {
                return new ConvertConfig(Depth - 1, parent);
            }
        } 
        #endregion
    }

    //public abstract class BaseDomainModel<TDomainModel, TDTO> : IDomainModel
    //    where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
    //    where TDTO : BaseDTO<TDTO>, new()
    //{
    //    #region Public Abstract Property(s).
    //    /// <summary>
    //    /// Entity ID.
    //    /// </summary>
    //    public abstract int ID { get; set; }
    //    #endregion

    //    #region Public Abstract Member(s).
    //    public TDTO CreateDTO(ConvertConfig config)
    //    {
    //        var dto = BuildDTO();
    //        return dto;
    //    }

    //    /// <summary>
    //    /// Construct a DTO of type TDTO for this Domain Model.
    //    /// </summary>
    //    /// <returns>The Domain Model as a TDTO</returns>
    //    public abstract TDTO BuildDTO();

    //    /// <summary>
    //    /// Populates Domain Model from dto.
    //    /// </summary>
    //    /// <param name="dto"></param>
    //    /// <returns></returns>
    //    public abstract void PopulateFromDTO(TDTO dto);
    //    #endregion

    //    #region Public Static Member(s).
    //    /// <summary>
    //    /// Creates Domain Model from DTO.
    //    /// </summary>
    //    /// <param name="dto"></param>
    //    /// <returns></returns>
    //    public static TDomainModel CreateFromDTO(TDTO dto)
    //    {
    //        var dm = new TDomainModel();
    //        dm.PopulateFromDTO(dto);
    //        return dm;
    //    }
    //    #endregion

    //    #region IDomainModel Implementation.
    //    dynamic IDomainModel.BuildDTO()
    //    {
    //        return BuildDTO();
    //    }
    //    #endregion
    //}

    //public class BaseChildDomainModel { }

    //public class BaseParentDomainModel { }



}
