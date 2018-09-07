using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Substrate;
using Substrate.Core;

namespace homework03gui
{
    public class mapReader : INotifyPropertyChanged
    {
        NbtWorld nbtWorld;
        IChunkManager chunkManager;
        IEnumerator<ChunkRef> chunkEnumerator;
        public ObservableCollection<int> currentSlice;

        public ObservableCollection<int> CurrentSlice
        {
            get { return currentSlice; }
            set
            {
                currentSlice = value;
                NotifyPropertyChanged();
            }
        }

        private ChunkRef currentChunk;
        public ChunkRef CurrentChunk
        {
            get
            {
                return currentChunk;
            }
            set
            {
                if (value != null)
                {
                    currentChunk = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int currentHeight;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CurrentHeight
        {
            get { return currentHeight; }
            set {
                currentHeight = value;
            }
        }

        public mapReader()
        {

        }

        public mapReader(string mapFilePath)
        {
            nbtWorld = NbtWorld.Open(mapFilePath);
            chunkManager = nbtWorld.GetChunkManager();
            chunkEnumerator = chunkManager.GetEnumerator();
            chunkEnumerator.MoveNext();
            CurrentChunk = chunkEnumerator.Current;
            CurrentHeight = 0;
            readASlice();
        }

        internal bool goNorth()
        {
            if(CurrentChunk.GetNorthNeighbor() != null)
            {
                CurrentChunk = CurrentChunk.GetNorthNeighbor();
                readASlice();
                return true;
            }
            return false;
        }

        internal bool goSouth()
        {
            if (CurrentChunk.GetSouthNeighbor() != null)
            {
                CurrentChunk = CurrentChunk.GetSouthNeighbor();
                readASlice();
                return true;
            }
            return false;
        }

        internal bool goEast()
        {
            if (CurrentChunk.GetEastNeighbor() != null)
            {
                CurrentChunk = CurrentChunk.GetEastNeighbor();
                readASlice();
                return true;
            }
            return false;
        }

        internal bool goWest()
        {
            if (CurrentChunk.GetWestNeighbor() != null)
            {
                CurrentChunk = CurrentChunk.GetWestNeighbor();
                readASlice();
                return true;
            }
            return false;
        }

        public void readASlice()
        {
            int xdim = CurrentChunk.Blocks.XDim;
            int ydim = CurrentChunk.Blocks.YDim;
            int zdim = CurrentChunk.Blocks.ZDim;

            ObservableCollection<int> slice = new ObservableCollection<int>();

            for (int z = 0; z < zdim; z++)      //length
            {
                for (int x = 0; x < xdim; x++)  //width
                {
                    slice.Add(CurrentChunk.Blocks.GetID(x, CurrentHeight, z));
                }
            }

            CurrentSlice = slice;
        }
    }
}
