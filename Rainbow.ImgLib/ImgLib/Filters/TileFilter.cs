﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rainbow.ImgLib.Filters
{
    public class TileFilter : ImageFilter
    {
        private int bpp;
        private int tileWidth;
        private int tileHeight;
        private int width, height;

        public TileFilter(int bpp, int tileWidth, int tileHeight, int width, int height)
        {
            this.bpp = bpp;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.width = width;
            this.height = height;
        }

        public override byte[] ApplyFilter(byte[] originalData, int index, int length)
        {
            /*byte[] newData = new byte[length];

            int lineSize = (tileWidth * bpp) / 8;
            int tileSize = lineSize * tileHeight;
            int pitch = (width * bpp) / 8;

            int tile = 0;

            for (int y = 0; y < height; y += tileHeight)
            {
                for (int x = 0; x < pitch; x += lineSize)
                {
                    for (int line = 0; line < tileHeight; line++)
                    {
                        Array.Copy(originalData, pitch * (y + line) + x, newData, index + tile * tileSize + line * lineSize, lineSize);
                    }

                    tile++;
                }
            }

            return newData;*/

            byte[] Buf = new byte[length];
            int w = (this.width * bpp) / 8;
            int lineSize = (tileWidth * bpp) / 8;
            int tileSize = lineSize * tileHeight;

            int rowblocks = w / lineSize;

            int totalBlocksx = w / lineSize;
            int totalBlocksy = height / tileHeight;

            for (int blocky = 0; blocky < totalBlocksy; blocky++)
                for (int blockx = 0; blockx < totalBlocksx; blockx++)
                {
                    int block_index = blockx + blocky * rowblocks;
                    int block_address = block_index * tileSize;

                    for (int y = 0; y < tileHeight; y++)
                    {
                        int absolutey = y + blocky * tileHeight;
                        Array.Copy(originalData, index + blockx * lineSize + absolutey * w , Buf, + block_address + y * lineSize, lineSize);
                    }
                }

            int start = totalBlocksy * rowblocks * lineSize * 8;
            for (int i = start; i < length; i++)
                Buf[i] = originalData[i + index];

            return Buf;
        }

        public override byte[] Defilter(byte[] originalData, int index, int length)
        {
            /*byte[] newData = new byte[length];

            int lineSize = (tileWidth * bpp) / 8;
            int tileSize = lineSize * tileHeight;
            int pitch = (width * bpp) / 8;

            int tile = 0;

            for (int y = 0; y < height; y += tileHeight)
            {
                for (int x = 0; x < pitch; x += lineSize)
                {
                    for (int line = 0; line < tileHeight; line++)
                    {
                        Array.Copy(originalData, index + tile * tileSize + line * lineSize, newData, pitch * (y + line) + x, lineSize);
                    }

                    tile++;
                }
            }

            return newData;*/

            byte[] Buf = new byte[length];
            int w = (this.width * bpp) / 8;
            int lineSize = (tileWidth * bpp) / 8;
            int tileSize = lineSize * tileHeight;

            int rowblocks = w / lineSize;

            int totalBlocksx = w / lineSize;
            int totalBlocksy = height / tileHeight;

            for (int blocky = 0; blocky < totalBlocksy; blocky++)
                for (int blockx = 0; blockx < totalBlocksx; blockx++)
                {
                    int block_index = blockx + blocky * rowblocks;
                    int block_address = block_index * tileSize;

                    for (int y = 0; y < tileHeight; y++)
                    {
                        int absolutey = y + blocky * tileHeight;
                        Array.Copy(originalData, index + block_address + y * lineSize, Buf, blockx * lineSize + absolutey * w, lineSize);
                    }
                }

            int start = totalBlocksy * rowblocks * lineSize * 8;
            for (int i = start; i < length; i++)
                Buf[i] = originalData[i + index];

            return Buf;
        }
    }
}
