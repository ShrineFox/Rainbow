﻿using System.Collections.Generic;
using System.Linq;

namespace Rainbow.ImgLib.Formats
{
    public class TIM2Texture : TextureFormatBase
    {
        #region Members

        private List<TIM2Segment> imagesList;

        #endregion

        internal TIM2Texture(List<TIM2Segment> list) 
        {
            imagesList = list;
        }

        #region Properties

        public bool Swizzled
        {
            get { return imagesList.First().Swizzled; }
            set
            {
                foreach (TIM2Segment tim2 in imagesList)
                    tim2.Swizzled = value;
            }
        }

        public int Bpp { get { return imagesList[GetActiveFrame()].Bpp;  } }

        public int Version { get; internal set; }

        /// <inheritdoc />
        public override int FramesCount
        {
            get
            {
                return imagesList.Count;
            }
        }

        /// <inheritdoc />
        public override int PalettesCount
        {
            get
            {
                return imagesList[GetActiveFrame()].PalettesCount;
            }
        }

        #endregion

        /// <inheritdoc />
        protected override System.Drawing.Image GetImage(int activeFrame,int activePalette)
        {
            TIM2Segment tim2 = imagesList[activeFrame];
            return tim2.SelectActivePalette(activePalette).GetImage();
        }

        internal List<TIM2Segment> TIM2SegmentsList { get { return imagesList; } }
    }
}
