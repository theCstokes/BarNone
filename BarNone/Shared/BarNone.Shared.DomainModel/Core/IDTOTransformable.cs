﻿using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DTOTransformable.Core
{
    public interface IDTOTransformable
    {
        dynamic CreateDTO(ConvertConfig config);
    }

    public interface IDTOTransformable<TDTO> : IDTOTransformable
        where TDTO : BaseDTO<TDTO>, new()
    {
        new TDTO CreateDTO(ConvertConfig config = null);

        void PopulateFromDTO(TDTO dto, ConvertConfig config);
    }
}
