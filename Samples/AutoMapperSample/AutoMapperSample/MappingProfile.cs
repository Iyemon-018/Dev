using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapperSample
{
    /// <summary>
    /// 独自マッピングのプロファイル設定
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// マッピングの設定
        /// </summary>
        /// <remarks>
        /// Configure()メソッドをオーバライドしてマッピングを設定していきます。
        /// </remarks>
        protected override void Configure()
        {
            // ここにマッピング定義を書いていく。
            Mapper.CreateMap<Source, Destination>()
                .ForMember(d => d.RemarksDummy, opt => opt.MapFrom(s => s.Remarks))
                .ReverseMap()
                .ForMember(s => s.Remarks, opt => opt.MapFrom(d => d.RemarksDummy));
        }
    }
}
