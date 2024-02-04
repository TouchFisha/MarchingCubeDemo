
using System;
using System.Reflection;
using Unity.Collections;
using Unity.Mathematics;

namespace NUtil.NMesh
{

    public struct MarchingCubeConstantBuffer
    {
        public static readonly NativeArray<int> case_to_numpolys = new NativeArray<int>(new int[] {
0,
1,
1,
2,
1,
2,
2,
3,
1,
2,
2,
3,
2,
3,
3,
2,
1,
2,
2,
3,
2,
3,
3,
4,
2,
3,
3,
4,
3,
4,
4,
3,

1,
2,
2,
3,
2,
3,
3,
4,
2,
3,
3,
4,
3,
4,
4,
3,
2,
3,
3,
2,
3,
4,
4,
3,
3,
4,
4,
3,
4,
5,
5,
2,

1,
2,
2,
3,
2,
3,
3,
4,
2,
3,
3,
4,
3,
4,
4,
3,
2,
3,
3,
4,
3,
4,
4,
5,
3,
4,
4,
5,
4,
5,
5,
4,

2,
3,
3,
4,
3,
4,
2,
3,
3,
4,
4,
5,
4,
5,
3,
2,
3,
4,
4,
3,
4,
5,
3,
2,
4,
5,
5,
4,
5,
2,
4,
1,

1,
2,
2,
3,
2,
3,
3,
4,
2,
3,
3,
4,
3,
4,
4,
3,
2,
3,
3,
4,
3,
4,
4,
5,
3,
2,
4,
3,
4,
3,
5,
2,

2,
3,
3,
4,
3,
4,
4,
5,
3,
4,
4,
5,
4,
5,
5,
4,
3,
4,
4,
3,
4,
5,
5,
4,
4,
3,
5,
2,
5,
4,
2,
1,

2,
3,
3,
4,
3,
4,
4,
5,
3,
4,
4,
5,
2,
3,
3,
2,
3,
4,
4,
5,
4,
5,
5,
2,
4,
3,
5,
4,
3,
2,
4,
1,
3,
4,
4,
5,
4,
5,
3,
4,
4,
5,
5,
2,
3,
4,
2,
1,
2,
3,
3,
2,
3,
4,
2,
1,
3,
2,
4,
1,
2,
1,
1,
0,
        }, Allocator.Persistent);

        public static readonly NativeArray<int3> edge_start = new NativeArray<int3>(new int3[]{
new int3(0, 0, 0 ),
new int3(0, 1, 0 ),
new int3(1, 0, 0 ),
new int3(0, 0, 0 ),

new int3(0, 0, 1 ),
new int3(0, 1, 1 ),
new int3(1, 0, 1 ),
new int3(0, 0, 1 ),

new int3(0, 0, 0 ),
new int3(0, 1, 0 ),
new int3(1, 1, 0 ),
new int3(1, 0, 0 ),
        }, Allocator.Persistent);
        public static readonly NativeArray<int3> edge_dir = new NativeArray<int3>(new int3[]{
new int3(0, 1, 0 ),
new int3(1, 0, 0 ),
new int3(0, 1, 0 ),
new int3(1, 0, 0 ),

new int3(0, 1, 0 ),
new int3(1, 0, 0 ),
new int3(0, 1, 0 ),
new int3(1, 0, 0 ),

new int3(0, 0, 1 ),
new int3(0, 0, 1 ),
new int3(0, 0, 1 ),
new int3(0, 0, 1 ),
        }, Allocator.Persistent);
        public static readonly NativeArray<int3> edge_end = new NativeArray<int3>(new int3[]{
new int3(0, 1, 0 ),
new int3(1, 1, 0 ),
new int3(1, 1, 0 ),
new int3(1, 0, 0 ),

new int3(0, 1, 1 ),
new int3(1, 1, 1 ),
new int3(1, 1, 1 ),
new int3(1, 0, 1 ),

new int3(0, 0, 1 ),
new int3(0, 1, 1 ),
new int3(1, 1, 1 ),
new int3(1, 0, 1 ),
        }, Allocator.Persistent);

        public static readonly NativeArray<int> edge_axis = new NativeArray<int>(new int[]{
1,
0,
1,
0,

1,
0,
1,
0,

2,
2,
2,
2,
}, Allocator.Persistent);

        public static readonly NativeArray<int4> g_triTable = new NativeArray<int4>(new int4[]{
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  8,  3, -1 ),
new int4(9,  8,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(1,  2, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  2, 10, -1 ),
new int4(0,  2,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  8,  3, -1 ),
new int4(2, 10,  8, -1 ),
new int4(10,  9,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 11,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0, 11,  2, -1 ),
new int4(8, 11,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  9,  0, -1 ),
new int4(2,  3, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1, 11,  2, -1 ),
new int4(1,  9, 11, -1 ),
new int4(9,  8, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 10,  1, -1 ),
new int4(11, 10,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0, 10,  1, -1 ),
new int4(0,  8, 10, -1 ),
new int4(8, 11, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  9,  0, -1 ),
new int4(3, 11,  9, -1 ),
new int4(11, 10,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  8, 10, -1 ),
new int4(10,  8, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  7,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  3,  0, -1 ),
new int4(7,  3,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(8,  4,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  1,  9, -1 ),
new int4(4,  7,  1, -1 ),
new int4(7,  3,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(8,  4,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  4,  7, -1 ),
new int4(3,  0,  4, -1 ),
new int4(1,  2, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  2, 10, -1 ),
new int4(9,  0,  2, -1 ),
new int4(8,  4,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2, 10,  9, -1 ),
new int4(2,  9,  7, -1 ),
new int4(2,  7,  3, -1 ),
new int4(7,  9,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  4,  7, -1 ),
new int4(3, 11,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  4,  7, -1 ),
new int4(11,  2,  4, -1 ),
new int4(2,  0,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  0,  1, -1 ),
new int4(8,  4,  7, -1 ),
new int4(2,  3, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  7, 11, -1 ),
new int4(9,  4, 11, -1 ),
new int4(9, 11,  2, -1 ),
new int4(9,  2,  1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 10,  1, -1 ),
new int4(3, 11, 10, -1 ),
new int4(7,  8,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1, 11, 10, -1 ),
new int4(1,  4, 11, -1 ),
new int4(1,  0,  4, -1 ),
new int4(7, 11,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  7,  8, -1 ),
new int4(9,  0, 11, -1 ),
new int4(9, 11, 10, -1 ),
new int4(11,  0,  3, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  7, 11, -1 ),
new int4(4, 11,  9, -1 ),
new int4(9, 11, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  4, -1 ),
new int4(0,  8,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  5,  4, -1 ),
new int4(1,  5,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  5,  4, -1 ),
new int4(8,  3,  5, -1 ),
new int4(3,  1,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(9,  5,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  0,  8, -1 ),
new int4(1,  2, 10, -1 ),
new int4(4,  9,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  2, 10, -1 ),
new int4(5,  4,  2, -1 ),
new int4(4,  0,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2, 10,  5, -1 ),
new int4(3,  2,  5, -1 ),
new int4(3,  5,  4, -1 ),
new int4(3,  4,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  4, -1 ),
new int4(2,  3, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0, 11,  2, -1 ),
new int4(0,  8, 11, -1 ),
new int4(4,  9,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  5,  4, -1 ),
new int4(0,  1,  5, -1 ),
new int4(2,  3, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  1,  5, -1 ),
new int4(2,  5,  8, -1 ),
new int4(2,  8, 11, -1 ),
new int4(4,  8,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  3, 11, -1 ),
new int4(10,  1,  3, -1 ),
new int4(9,  5,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  9,  5, -1 ),
new int4(0,  8,  1, -1 ),
new int4(8, 10,  1, -1 ),
new int4(8, 11, 10, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  4,  0, -1 ),
new int4(5,  0, 11, -1 ),
new int4(5, 11, 10, -1 ),
new int4(11,  0,  3, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  4,  8, -1 ),
new int4(5,  8, 10, -1 ),
new int4(10,  8, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  7,  8, -1 ),
new int4(5,  7,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  3,  0, -1 ),
new int4(9,  5,  3, -1 ),
new int4(5,  7,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  7,  8, -1 ),
new int4(0,  1,  7, -1 ),
new int4(1,  5,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  5,  3, -1 ),
new int4(3,  5,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  7,  8, -1 ),
new int4(9,  5,  7, -1 ),
new int4(10,  1,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  1,  2, -1 ),
new int4(9,  5,  0, -1 ),
new int4(5,  3,  0, -1 ),
new int4(5,  7,  3, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  0,  2, -1 ),
new int4(8,  2,  5, -1 ),
new int4(8,  5,  7, -1 ),
new int4(10,  5,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2, 10,  5, -1 ),
new int4(2,  5,  3, -1 ),
new int4(3,  5,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  9,  5, -1 ),
new int4(7,  8,  9, -1 ),
new int4(3, 11,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  7, -1 ),
new int4(9,  7,  2, -1 ),
new int4(9,  2,  0, -1 ),
new int4(2,  7, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  3, 11, -1 ),
new int4(0,  1,  8, -1 ),
new int4(1,  7,  8, -1 ),
new int4(1,  5,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  2,  1, -1 ),
new int4(11,  1,  7, -1 ),
new int4(7,  1,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  8, -1 ),
new int4(8,  5,  7, -1 ),
new int4(10,  1,  3, -1 ),
new int4(10,  3, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  7,  0, -1 ),
new int4(5,  0,  9, -1 ),
new int4(7, 11,  0, -1 ),
new int4(1,  0, 10, -1 ),
new int4(11, 10,  0, -1 ),

new int4(11, 10,  0, -1 ),
new int4(11,  0,  3, -1 ),
new int4(10,  5,  0, -1 ),
new int4(8,  0,  7, -1 ),
new int4(5,  7,  0, -1 ),

new int4(11, 10,  5, -1 ),
new int4(7, 11,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  6,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(5, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  0,  1, -1 ),
new int4(5, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  8,  3, -1 ),
new int4(1,  9,  8, -1 ),
new int4(5, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  6,  5, -1 ),
new int4(2,  6,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  6,  5, -1 ),
new int4(1,  2,  6, -1 ),
new int4(3,  0,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  6,  5, -1 ),
new int4(9,  0,  6, -1 ),
new int4(0,  2,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  9,  8, -1 ),
new int4(5,  8,  2, -1 ),
new int4(5,  2,  6, -1 ),
new int4(3,  2,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  3, 11, -1 ),
new int4(10,  6,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  0,  8, -1 ),
new int4(11,  2,  0, -1 ),
new int4(10,  6,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(2,  3, 11, -1 ),
new int4(5, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5, 10,  6, -1 ),
new int4(1,  9,  2, -1 ),
new int4(9, 11,  2, -1 ),
new int4(9,  8, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  3, 11, -1 ),
new int4(6,  5,  3, -1 ),
new int4(5,  1,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8, 11, -1 ),
new int4(0, 11,  5, -1 ),
new int4(0,  5,  1, -1 ),
new int4(5, 11,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 11,  6, -1 ),
new int4(0,  3,  6, -1 ),
new int4(0,  6,  5, -1 ),
new int4(0,  5,  9, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  5,  9, -1 ),
new int4(6,  9, 11, -1 ),
new int4(11,  9,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5, 10,  6, -1 ),
new int4(4,  7,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  3,  0, -1 ),
new int4(4,  7,  3, -1 ),
new int4(6,  5, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  9,  0, -1 ),
new int4(5, 10,  6, -1 ),
new int4(8,  4,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  6,  5, -1 ),
new int4(1,  9,  7, -1 ),
new int4(1,  7,  3, -1 ),
new int4(7,  9,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  1,  2, -1 ),
new int4(6,  5,  1, -1 ),
new int4(4,  7,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2,  5, -1 ),
new int4(5,  2,  6, -1 ),
new int4(3,  0,  4, -1 ),
new int4(3,  4,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  4,  7, -1 ),
new int4(9,  0,  5, -1 ),
new int4(0,  6,  5, -1 ),
new int4(0,  2,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  3,  9, -1 ),
new int4(7,  9,  4, -1 ),
new int4(3,  2,  9, -1 ),
new int4(5,  9,  6, -1 ),
new int4(2,  6,  9, -1 ),

new int4(3, 11,  2, -1 ),
new int4(7,  8,  4, -1 ),
new int4(10,  6,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5, 10,  6, -1 ),
new int4(4,  7,  2, -1 ),
new int4(4,  2,  0, -1 ),
new int4(2,  7, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(4,  7,  8, -1 ),
new int4(2,  3, 11, -1 ),
new int4(5, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  2,  1, -1 ),
new int4(9, 11,  2, -1 ),
new int4(9,  4, 11, -1 ),
new int4(7, 11,  4, -1 ),
new int4(5, 10,  6, -1 ),

new int4(8,  4,  7, -1 ),
new int4(3, 11,  5, -1 ),
new int4(3,  5,  1, -1 ),
new int4(5, 11,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  1, 11, -1 ),
new int4(5, 11,  6, -1 ),
new int4(1,  0, 11, -1 ),
new int4(7, 11,  4, -1 ),
new int4(0,  4, 11, -1 ),

new int4(0,  5,  9, -1 ),
new int4(0,  6,  5, -1 ),
new int4(0,  3,  6, -1 ),
new int4(11,  6,  3, -1 ),
new int4(8,  4,  7, -1 ),

new int4(6,  5,  9, -1 ),
new int4(6,  9, 11, -1 ),
new int4(4,  7,  9, -1 ),
new int4(7, 11,  9, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  4,  9, -1 ),
new int4(6,  4, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4, 10,  6, -1 ),
new int4(4,  9, 10, -1 ),
new int4(0,  8,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  0,  1, -1 ),
new int4(10,  6,  0, -1 ),
new int4(6,  4,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  3,  1, -1 ),
new int4(8,  1,  6, -1 ),
new int4(8,  6,  4, -1 ),
new int4(6,  1, 10, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  4,  9, -1 ),
new int4(1,  2,  4, -1 ),
new int4(2,  6,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  0,  8, -1 ),
new int4(1,  2,  9, -1 ),
new int4(2,  4,  9, -1 ),
new int4(2,  6,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  2,  4, -1 ),
new int4(4,  2,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  3,  2, -1 ),
new int4(8,  2,  4, -1 ),
new int4(4,  2,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  4,  9, -1 ),
new int4(10,  6,  4, -1 ),
new int4(11,  2,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  2, -1 ),
new int4(2,  8, 11, -1 ),
new int4(4,  9, 10, -1 ),
new int4(4, 10,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 11,  2, -1 ),
new int4(0,  1,  6, -1 ),
new int4(0,  6,  4, -1 ),
new int4(6,  1, 10, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  4,  1, -1 ),
new int4(6,  1, 10, -1 ),
new int4(4,  8,  1, -1 ),
new int4(2,  1, 11, -1 ),
new int4(8, 11,  1, -1 ),

new int4(9,  6,  4, -1 ),
new int4(9,  3,  6, -1 ),
new int4(9,  1,  3, -1 ),
new int4(11,  6,  3, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8, 11,  1, -1 ),
new int4(8,  1,  0, -1 ),
new int4(11,  6,  1, -1 ),
new int4(9,  1,  4, -1 ),
new int4(6,  4,  1, -1 ),

new int4(3, 11,  6, -1 ),
new int4(3,  6,  0, -1 ),
new int4(0,  6,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  4,  8, -1 ),
new int4(11,  6,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7, 10,  6, -1 ),
new int4(7,  8, 10, -1 ),
new int4(8,  9, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  7,  3, -1 ),
new int4(0, 10,  7, -1 ),
new int4(0,  9, 10, -1 ),
new int4(6,  7, 10, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  6,  7, -1 ),
new int4(1, 10,  7, -1 ),
new int4(1,  7,  8, -1 ),
new int4(1,  8,  0, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  6,  7, -1 ),
new int4(10,  7,  1, -1 ),
new int4(1,  7,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2,  6, -1 ),
new int4(1,  6,  8, -1 ),
new int4(1,  8,  9, -1 ),
new int4(8,  6,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  6,  9, -1 ),
new int4(2,  9,  1, -1 ),
new int4(6,  7,  9, -1 ),
new int4(0,  9,  3, -1 ),
new int4(7,  3,  9, -1 ),

new int4(7,  8,  0, -1 ),
new int4(7,  0,  6, -1 ),
new int4(6,  0,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  3,  2, -1 ),
new int4(6,  7,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  3, 11, -1 ),
new int4(10,  6,  8, -1 ),
new int4(10,  8,  9, -1 ),
new int4(8,  6,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  0,  7, -1 ),
new int4(2,  7, 11, -1 ),
new int4(0,  9,  7, -1 ),
new int4(6,  7, 10, -1 ),
new int4(9, 10,  7, -1 ),

new int4(1,  8,  0, -1 ),
new int4(1,  7,  8, -1 ),
new int4(1, 10,  7, -1 ),
new int4(6,  7, 10, -1 ),
new int4(2,  3, 11, -1 ),

new int4(11,  2,  1, -1 ),
new int4(11,  1,  7, -1 ),
new int4(10,  6,  1, -1 ),
new int4(6,  7,  1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  9,  6, -1 ),
new int4(8,  6,  7, -1 ),
new int4(9,  1,  6, -1 ),
new int4(11,  6,  3, -1 ),
new int4(1,  3,  6, -1 ),

new int4(0,  9,  1, -1 ),
new int4(11,  6,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  8,  0, -1 ),
new int4(7,  0,  6, -1 ),
new int4(3, 11,  0, -1 ),
new int4(11,  6,  0, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7, 11,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  6, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  0,  8, -1 ),
new int4(11,  7,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(11,  7,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  1,  9, -1 ),
new int4(8,  3,  1, -1 ),
new int4(11,  7,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  1,  2, -1 ),
new int4(6, 11,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(3,  0,  8, -1 ),
new int4(6, 11,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  9,  0, -1 ),
new int4(2, 10,  9, -1 ),
new int4(6, 11,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6, 11,  7, -1 ),
new int4(2, 10,  3, -1 ),
new int4(10,  8,  3, -1 ),
new int4(10,  9,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  2,  3, -1 ),
new int4(6,  2,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  0,  8, -1 ),
new int4(7,  6,  0, -1 ),
new int4(6,  2,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  7,  6, -1 ),
new int4(2,  3,  7, -1 ),
new int4(0,  1,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  6,  2, -1 ),
new int4(1,  8,  6, -1 ),
new int4(1,  9,  8, -1 ),
new int4(8,  7,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  7,  6, -1 ),
new int4(10,  1,  7, -1 ),
new int4(1,  3,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  7,  6, -1 ),
new int4(1,  7, 10, -1 ),
new int4(1,  8,  7, -1 ),
new int4(1,  0,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  3,  7, -1 ),
new int4(0,  7, 10, -1 ),
new int4(0, 10,  9, -1 ),
new int4(6, 10,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  6, 10, -1 ),
new int4(7, 10,  8, -1 ),
new int4(8, 10,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  8,  4, -1 ),
new int4(11,  8,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  6, 11, -1 ),
new int4(3,  0,  6, -1 ),
new int4(0,  4,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  6, 11, -1 ),
new int4(8,  4,  6, -1 ),
new int4(9,  0,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  4,  6, -1 ),
new int4(9,  6,  3, -1 ),
new int4(9,  3,  1, -1 ),
new int4(11,  3,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  8,  4, -1 ),
new int4(6, 11,  8, -1 ),
new int4(2, 10,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(3,  0, 11, -1 ),
new int4(0,  6, 11, -1 ),
new int4(0,  4,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4, 11,  8, -1 ),
new int4(4,  6, 11, -1 ),
new int4(0,  2,  9, -1 ),
new int4(2, 10,  9, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  9,  3, -1 ),
new int4(10,  3,  2, -1 ),
new int4(9,  4,  3, -1 ),
new int4(11,  3,  6, -1 ),
new int4(4,  6,  3, -1 ),

new int4(8,  2,  3, -1 ),
new int4(8,  4,  2, -1 ),
new int4(4,  6,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  4,  2, -1 ),
new int4(4,  6,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  9,  0, -1 ),
new int4(2,  3,  4, -1 ),
new int4(2,  4,  6, -1 ),
new int4(4,  3,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  9,  4, -1 ),
new int4(1,  4,  2, -1 ),
new int4(2,  4,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  1,  3, -1 ),
new int4(8,  6,  1, -1 ),
new int4(8,  4,  6, -1 ),
new int4(6, 10,  1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  1,  0, -1 ),
new int4(10,  0,  6, -1 ),
new int4(6,  0,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  6,  3, -1 ),
new int4(4,  3,  8, -1 ),
new int4(6, 10,  3, -1 ),
new int4(0,  3,  9, -1 ),
new int4(10,  9,  3, -1 ),

new int4(10,  9,  4, -1 ),
new int4(6, 10,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  9,  5, -1 ),
new int4(7,  6, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(4,  9,  5, -1 ),
new int4(11,  7,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  0,  1, -1 ),
new int4(5,  4,  0, -1 ),
new int4(7,  6, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  7,  6, -1 ),
new int4(8,  3,  4, -1 ),
new int4(3,  5,  4, -1 ),
new int4(3,  1,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  4, -1 ),
new int4(10,  1,  2, -1 ),
new int4(7,  6, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6, 11,  7, -1 ),
new int4(1,  2, 10, -1 ),
new int4(0,  8,  3, -1 ),
new int4(4,  9,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  6, 11, -1 ),
new int4(5,  4, 10, -1 ),
new int4(4,  2, 10, -1 ),
new int4(4,  0,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  4,  8, -1 ),
new int4(3,  5,  4, -1 ),
new int4(3,  2,  5, -1 ),
new int4(10,  5,  2, -1 ),
new int4(11,  7,  6, -1 ),

new int4(7,  2,  3, -1 ),
new int4(7,  6,  2, -1 ),
new int4(5,  4,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  4, -1 ),
new int4(0,  8,  6, -1 ),
new int4(0,  6,  2, -1 ),
new int4(6,  8,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  6,  2, -1 ),
new int4(3,  7,  6, -1 ),
new int4(1,  5,  0, -1 ),
new int4(5,  4,  0, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  2,  8, -1 ),
new int4(6,  8,  7, -1 ),
new int4(2,  1,  8, -1 ),
new int4(4,  8,  5, -1 ),
new int4(1,  5,  8, -1 ),

new int4(9,  5,  4, -1 ),
new int4(10,  1,  6, -1 ),
new int4(1,  7,  6, -1 ),
new int4(1,  3,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  6, 10, -1 ),
new int4(1,  7,  6, -1 ),
new int4(1,  0,  7, -1 ),
new int4(8,  7,  0, -1 ),
new int4(9,  5,  4, -1 ),

new int4(4,  0, 10, -1 ),
new int4(4, 10,  5, -1 ),
new int4(0,  3, 10, -1 ),
new int4(6, 10,  7, -1 ),
new int4(3,  7, 10, -1 ),

new int4(7,  6, 10, -1 ),
new int4(7, 10,  8, -1 ),
new int4(5,  4, 10, -1 ),
new int4(4,  8, 10, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6,  9,  5, -1 ),
new int4(6, 11,  9, -1 ),
new int4(11,  8,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  6, 11, -1 ),
new int4(0,  6,  3, -1 ),
new int4(0,  5,  6, -1 ),
new int4(0,  9,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0, 11,  8, -1 ),
new int4(0,  5, 11, -1 ),
new int4(0,  1,  5, -1 ),
new int4(5,  6, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(6, 11,  3, -1 ),
new int4(6,  3,  5, -1 ),
new int4(5,  3,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 10, -1 ),
new int4(9,  5, 11, -1 ),
new int4(9, 11,  8, -1 ),
new int4(11,  5,  6, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0, 11,  3, -1 ),
new int4(0,  6, 11, -1 ),
new int4(0,  9,  6, -1 ),
new int4(5,  6,  9, -1 ),
new int4(1,  2, 10, -1 ),

new int4(11,  8,  5, -1 ),
new int4(11,  5,  6, -1 ),
new int4(8,  0,  5, -1 ),
new int4(10,  5,  2, -1 ),
new int4(0,  2,  5, -1 ),

new int4(6, 11,  3, -1 ),
new int4(6,  3,  5, -1 ),
new int4(2, 10,  3, -1 ),
new int4(10,  5,  3, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  8,  9, -1 ),
new int4(5,  2,  8, -1 ),
new int4(5,  6,  2, -1 ),
new int4(3,  8,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  5,  6, -1 ),
new int4(9,  6,  0, -1 ),
new int4(0,  6,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  5,  8, -1 ),
new int4(1,  8,  0, -1 ),
new int4(5,  6,  8, -1 ),
new int4(3,  8,  2, -1 ),
new int4(6,  2,  8, -1 ),

new int4(1,  5,  6, -1 ),
new int4(2,  1,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  3,  6, -1 ),
new int4(1,  6, 10, -1 ),
new int4(3,  8,  6, -1 ),
new int4(5,  6,  9, -1 ),
new int4(8,  9,  6, -1 ),

new int4(10,  1,  0, -1 ),
new int4(10,  0,  6, -1 ),
new int4(9,  5,  0, -1 ),
new int4(5,  6,  0, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  3,  8, -1 ),
new int4(5,  6, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  5,  6, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  5, 10, -1 ),
new int4(7,  5, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  5, 10, -1 ),
new int4(11,  7,  5, -1 ),
new int4(8,  3,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5, 11,  7, -1 ),
new int4(5, 10, 11, -1 ),
new int4(1,  9,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10,  7,  5, -1 ),
new int4(10, 11,  7, -1 ),
new int4(9,  8,  1, -1 ),
new int4(8,  3,  1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  1,  2, -1 ),
new int4(11,  7,  1, -1 ),
new int4(7,  5,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(1,  2,  7, -1 ),
new int4(1,  7,  5, -1 ),
new int4(7,  2, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  7,  5, -1 ),
new int4(9,  2,  7, -1 ),
new int4(9,  0,  2, -1 ),
new int4(2, 11,  7, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(7,  5,  2, -1 ),
new int4(7,  2, 11, -1 ),
new int4(5,  9,  2, -1 ),
new int4(3,  2,  8, -1 ),
new int4(9,  8,  2, -1 ),

new int4(2,  5, 10, -1 ),
new int4(2,  3,  5, -1 ),
new int4(3,  7,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  2,  0, -1 ),
new int4(8,  5,  2, -1 ),
new int4(8,  7,  5, -1 ),
new int4(10,  2,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  0,  1, -1 ),
new int4(5, 10,  3, -1 ),
new int4(5,  3,  7, -1 ),
new int4(3, 10,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  8,  2, -1 ),
new int4(9,  2,  1, -1 ),
new int4(8,  7,  2, -1 ),
new int4(10,  2,  5, -1 ),
new int4(7,  5,  2, -1 ),

new int4(1,  3,  5, -1 ),
new int4(3,  7,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  7, -1 ),
new int4(0,  7,  1, -1 ),
new int4(1,  7,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  0,  3, -1 ),
new int4(9,  3,  5, -1 ),
new int4(5,  3,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  8,  7, -1 ),
new int4(5,  9,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  8,  4, -1 ),
new int4(5, 10,  8, -1 ),
new int4(10, 11,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5,  0,  4, -1 ),
new int4(5, 11,  0, -1 ),
new int4(5, 10, 11, -1 ),
new int4(11,  3,  0, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1,  9, -1 ),
new int4(8,  4, 10, -1 ),
new int4(8, 10, 11, -1 ),
new int4(10,  4,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(10, 11,  4, -1 ),
new int4(10,  4,  5, -1 ),
new int4(11,  3,  4, -1 ),
new int4(9,  4,  1, -1 ),
new int4(3,  1,  4, -1 ),

new int4(2,  5,  1, -1 ),
new int4(2,  8,  5, -1 ),
new int4(2, 11,  8, -1 ),
new int4(4,  5,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  4, 11, -1 ),
new int4(0, 11,  3, -1 ),
new int4(4,  5, 11, -1 ),
new int4(2, 11,  1, -1 ),
new int4(5,  1, 11, -1 ),

new int4(0,  2,  5, -1 ),
new int4(0,  5,  9, -1 ),
new int4(2, 11,  5, -1 ),
new int4(4,  5,  8, -1 ),
new int4(11,  8,  5, -1 ),

new int4(9,  4,  5, -1 ),
new int4(2, 11,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  5, 10, -1 ),
new int4(3,  5,  2, -1 ),
new int4(3,  4,  5, -1 ),
new int4(3,  8,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(5, 10,  2, -1 ),
new int4(5,  2,  4, -1 ),
new int4(4,  2,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3, 10,  2, -1 ),
new int4(3,  5, 10, -1 ),
new int4(3,  8,  5, -1 ),
new int4(4,  5,  8, -1 ),
new int4(0,  1,  9, -1 ),

new int4(5, 10,  2, -1 ),
new int4(5,  2,  4, -1 ),
new int4(1,  9,  2, -1 ),
new int4(9,  4,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  4,  5, -1 ),
new int4(8,  5,  3, -1 ),
new int4(3,  5,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  4,  5, -1 ),
new int4(1,  0,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(8,  4,  5, -1 ),
new int4(8,  5,  3, -1 ),
new int4(9,  0,  5, -1 ),
new int4(0,  3,  5, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  4,  5, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4, 11,  7, -1 ),
new int4(4,  9, 11, -1 ),
new int4(9, 10, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  8,  3, -1 ),
new int4(4,  9,  7, -1 ),
new int4(9, 11,  7, -1 ),
new int4(9, 10, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1, 10, 11, -1 ),
new int4(1, 11,  4, -1 ),
new int4(1,  4,  0, -1 ),
new int4(7,  4, 11, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  1,  4, -1 ),
new int4(3,  4,  8, -1 ),
new int4(1, 10,  4, -1 ),
new int4(7,  4, 11, -1 ),
new int4(10, 11,  4, -1 ),

new int4(4, 11,  7, -1 ),
new int4(9, 11,  4, -1 ),
new int4(9,  2, 11, -1 ),
new int4(9,  1,  2, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9,  7,  4, -1 ),
new int4(9, 11,  7, -1 ),
new int4(9,  1, 11, -1 ),
new int4(2, 11,  1, -1 ),
new int4(0,  8,  3, -1 ),

new int4(11,  7,  4, -1 ),
new int4(11,  4,  2, -1 ),
new int4(2,  4,  0, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(11,  7,  4, -1 ),
new int4(11,  4,  2, -1 ),
new int4(8,  3,  4, -1 ),
new int4(3,  2,  4, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  9, 10, -1 ),
new int4(2,  7,  9, -1 ),
new int4(2,  3,  7, -1 ),
new int4(7,  4,  9, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9, 10,  7, -1 ),
new int4(9,  7,  4, -1 ),
new int4(10,  2,  7, -1 ),
new int4(8,  7,  0, -1 ),
new int4(2,  0,  7, -1 ),

new int4(3,  7, 10, -1 ),
new int4(3, 10,  2, -1 ),
new int4(7,  4, 10, -1 ),
new int4(1, 10,  0, -1 ),
new int4(4,  0, 10, -1 ),

new int4(1, 10,  2, -1 ),
new int4(8,  7,  4, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  9,  1, -1 ),
new int4(4,  1,  7, -1 ),
new int4(7,  1,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  9,  1, -1 ),
new int4(4,  1,  7, -1 ),
new int4(0,  8,  1, -1 ),
new int4(8,  7,  1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  0,  3, -1 ),
new int4(7,  4,  3, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(4,  8,  7, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9, 10,  8, -1 ),
new int4(10, 11,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  0,  9, -1 ),
new int4(3,  9, 11, -1 ),
new int4(11,  9, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  1, 10, -1 ),
new int4(0, 10,  8, -1 ),
new int4(8, 10, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  1, 10, -1 ),
new int4(11,  3, 10, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  2, 11, -1 ),
new int4(1, 11,  9, -1 ),
new int4(9, 11,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  0,  9, -1 ),
new int4(3,  9, 11, -1 ),
new int4(1,  2,  9, -1 ),
new int4(2, 11,  9, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  2, 11, -1 ),
new int4(8,  0, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(3,  2, 11, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  3,  8, -1 ),
new int4(2,  8, 10, -1 ),
new int4(10,  8,  9, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(9, 10,  2, -1 ),
new int4(0,  9,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(2,  3,  8, -1 ),
new int4(2,  8, 10, -1 ),
new int4(0,  1,  8, -1 ),
new int4(1, 10,  8, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1, 10,  2, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(1,  3,  8, -1 ),
new int4(9,  1,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  9,  1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(0,  3,  8, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),
new int4(-1, -1, -1, -1 ),

        }, Allocator.Persistent);
        public static readonly NativeArray<int4> g_case_to_vertOnEdge_mask = new NativeArray<int4>(new int4[]{
new int4(0,    0, 0, 0 ),
new int4(265,  0, 0, 0 ),
new int4(515,  0, 0, 0 ),
new int4(778,  0, 0, 0 ),
new int4(1030, 0, 0, 0 ),
new int4(1295, 0, 0, 0 ),
new int4(1541, 0, 0, 0 ),
new int4(1804, 0, 0, 0 ),

new int4(2060, 0, 0, 0 ),
new int4(2309, 0, 0, 0 ),
new int4(2575, 0, 0, 0 ),
new int4(2822, 0, 0, 0 ),
new int4(3082, 0, 0, 0 ),
new int4(3331, 0, 0, 0 ),
new int4(3593, 0, 0, 0 ),
new int4(3840, 0, 0, 0 ),

new int4(400,  0, 0, 0 ),
new int4(153,  0, 0, 0 ),
new int4(915,  0, 0, 0 ),
new int4(666,  0, 0, 0 ),
new int4(1430, 0, 0, 0 ),
new int4(1183, 0, 0, 0 ),
new int4(1941, 0, 0, 0 ),
new int4(1692, 0, 0, 0 ),

new int4(2460, 0, 0, 0 ),
new int4(2197, 0, 0, 0 ),
new int4(2975, 0, 0, 0 ),
new int4(2710, 0, 0, 0 ),
new int4(3482, 0, 0, 0 ),
new int4(3219, 0, 0, 0 ),
new int4(3993, 0, 0, 0 ),
new int4(3728, 0, 0, 0 ),

new int4(560,  0, 0, 0 ),
new int4(825,  0, 0, 0 ),
new int4(51,   0, 0, 0 ),
new int4(314,  0, 0, 0 ),
new int4(1590, 0, 0, 0 ),
new int4(1855, 0, 0, 0 ),
new int4(1077, 0, 0, 0 ),
new int4(1340, 0, 0, 0 ),

new int4(2620, 0, 0, 0 ),
new int4(2869, 0, 0, 0 ),
new int4(2111, 0, 0, 0 ),
new int4(2358, 0, 0, 0 ),
new int4(3642, 0, 0, 0 ),
new int4(3891, 0, 0, 0 ),
new int4(3129, 0, 0, 0 ),
new int4(3376, 0, 0, 0 ),

new int4(928,  0, 0, 0 ),
new int4(681,  0, 0, 0 ),
new int4(419,  0, 0, 0 ),
new int4(170,  0, 0, 0 ),
new int4(1958, 0, 0, 0 ),
new int4(1711, 0, 0, 0 ),
new int4(1445, 0, 0, 0 ),
new int4(1196, 0, 0, 0 ),

new int4(2988, 0, 0, 0 ),
new int4(2725, 0, 0, 0 ),
new int4(2479, 0, 0, 0 ),
new int4(2214, 0, 0, 0 ),
new int4(4010, 0, 0, 0 ),
new int4(3747, 0, 0, 0 ),
new int4(3497, 0, 0, 0 ),
new int4(3232, 0, 0, 0 ),

new int4(1120, 0, 0, 0 ),
new int4(1385, 0, 0, 0 ),
new int4(1635, 0, 0, 0 ),
new int4(1898, 0, 0, 0 ),
new int4(102,  0, 0, 0 ),
new int4(367,  0, 0, 0 ),
new int4(613,  0, 0, 0 ),
new int4(876,  0, 0, 0 ),

new int4(3180, 0, 0, 0 ),
new int4(3429, 0, 0, 0 ),
new int4(3695, 0, 0, 0 ),
new int4(3942, 0, 0, 0 ),
new int4(2154, 0, 0, 0 ),
new int4(2403, 0, 0, 0 ),
new int4(2665, 0, 0, 0 ),
new int4(2912, 0, 0, 0 ),

new int4(1520, 0, 0, 0 ),
new int4(1273, 0, 0, 0 ),
new int4(2035, 0, 0, 0 ),
new int4(1786, 0, 0, 0 ),
new int4(502,  0, 0, 0 ),
new int4(255,  0, 0, 0 ),
new int4(1013, 0, 0, 0 ),
new int4(764,  0, 0, 0 ),

new int4(3580, 0, 0, 0 ),
new int4(3317, 0, 0, 0 ),
new int4(4095, 0, 0, 0 ),
new int4(3830, 0, 0, 0 ),
new int4(2554, 0, 0, 0 ),
new int4(2291, 0, 0, 0 ),
new int4(3065, 0, 0, 0 ),
new int4(2800, 0, 0, 0 ),

new int4(1616, 0, 0, 0 ),
new int4(1881, 0, 0, 0 ),
new int4(1107, 0, 0, 0 ),
new int4(1370, 0, 0, 0 ),
new int4(598,  0, 0, 0 ),
new int4(863,  0, 0, 0 ),
new int4(85,   0, 0, 0 ),
new int4(348,  0, 0, 0 ),

new int4(3676, 0, 0, 0 ),
new int4(3925, 0, 0, 0 ),
new int4(3167, 0, 0, 0 ),
new int4(3414, 0, 0, 0 ),
new int4(2650, 0, 0, 0 ),
new int4(2899, 0, 0, 0 ),
new int4(2137, 0, 0, 0 ),
new int4(2384, 0, 0, 0 ),

new int4(1984, 0, 0, 0 ),
new int4(1737, 0, 0, 0 ),
new int4(1475, 0, 0, 0 ),
new int4(1226, 0, 0, 0 ),
new int4(966,  0, 0, 0 ),
new int4(719,  0, 0, 0 ),
new int4(453,  0, 0, 0 ),
new int4(204,  0, 0, 0 ),

new int4(4044, 0, 0, 0 ),
new int4(3781, 0, 0, 0 ),
new int4(3535, 0, 0, 0 ),
new int4(3270, 0, 0, 0 ),
new int4(3018, 0, 0, 0 ),
new int4(2755, 0, 0, 0 ),
new int4(2505, 0, 0, 0 ),
new int4(2240, 0, 0, 0 ),

new int4(2240, 0, 0, 0 ),
new int4(2505, 0, 0, 0 ),
new int4(2755, 0, 0, 0 ),
new int4(3018, 0, 0, 0 ),
new int4(3270, 0, 0, 0 ),
new int4(3535, 0, 0, 0 ),
new int4(3781, 0, 0, 0 ),
new int4(4044, 0, 0, 0 ),

new int4(204,  0, 0, 0 ),
new int4(453,  0, 0, 0 ),
new int4(719,  0, 0, 0 ),
new int4(966,  0, 0, 0 ),
new int4(1226, 0, 0, 0 ),
new int4(1475, 0, 0, 0 ),
new int4(1737, 0, 0, 0 ),
new int4(1984, 0, 0, 0 ),

new int4(2384, 0, 0, 0 ),
new int4(2137, 0, 0, 0 ),
new int4(2899, 0, 0, 0 ),
new int4(2650, 0, 0, 0 ),
new int4(3414, 0, 0, 0 ),
new int4(3167, 0, 0, 0 ),
new int4(3925, 0, 0, 0 ),
new int4(3676, 0, 0, 0 ),

new int4(348,  0, 0, 0 ),
new int4(85,   0, 0, 0 ),
new int4(863,  0, 0, 0 ),
new int4(598,  0, 0, 0 ),
new int4(1370, 0, 0, 0 ),
new int4(1107, 0, 0, 0 ),
new int4(1881, 0, 0, 0 ),
new int4(1616, 0, 0, 0 ),

new int4(2800, 0, 0, 0 ),
new int4(3065, 0, 0, 0 ),
new int4(2291, 0, 0, 0 ),
new int4(2554, 0, 0, 0 ),
new int4(3830, 0, 0, 0 ),
new int4(4095, 0, 0, 0 ),
new int4(3317, 0, 0, 0 ),
new int4(3580, 0, 0, 0 ),

new int4(764,  0, 0, 0 ),
new int4(1013, 0, 0, 0 ),
new int4(255,  0, 0, 0 ),
new int4(502,  0, 0, 0 ),
new int4(1786, 0, 0, 0 ),
new int4(2035, 0, 0, 0 ),
new int4(1273, 0, 0, 0 ),
new int4(1520, 0, 0, 0 ),

new int4(2912, 0, 0, 0 ),
new int4(2665, 0, 0, 0 ),
new int4(2403, 0, 0, 0 ),
new int4(2154, 0, 0, 0 ),
new int4(3942, 0, 0, 0 ),
new int4(3695, 0, 0, 0 ),
new int4(3429, 0, 0, 0 ),
new int4(3180, 0, 0, 0 ),

new int4(876,  0, 0, 0 ),
new int4(613,  0, 0, 0 ),
new int4(367,  0, 0, 0 ),
new int4(102,  0, 0, 0 ),
new int4(1898, 0, 0, 0 ),
new int4(1635, 0, 0, 0 ),
new int4(1385, 0, 0, 0 ),
new int4(1120, 0, 0, 0 ),

new int4(3232, 0, 0, 0 ),
new int4(3497, 0, 0, 0 ),
new int4(3747, 0, 0, 0 ),
new int4(4010, 0, 0, 0 ),
new int4(2214, 0, 0, 0 ),
new int4(2479, 0, 0, 0 ),
new int4(2725, 0, 0, 0 ),
new int4(2988, 0, 0, 0 ),

new int4(1196, 0, 0, 0 ),
new int4(1445, 0, 0, 0 ),
new int4(1711, 0, 0, 0 ),
new int4(1958, 0, 0, 0 ),
new int4(170,  0, 0, 0 ),
new int4(419,  0, 0, 0 ),
new int4(681,  0, 0, 0 ),
new int4(928,  0, 0, 0 ),

new int4(3376, 0, 0, 0 ),
new int4(3129, 0, 0, 0 ),
new int4(3891, 0, 0, 0 ),
new int4(3642, 0, 0, 0 ),
new int4(2358, 0, 0, 0 ),
new int4(2111, 0, 0, 0 ),
new int4(2869, 0, 0, 0 ),
new int4(2620, 0, 0, 0 ),

new int4(1340, 0, 0, 0 ),
new int4(1077, 0, 0, 0 ),
new int4(1855, 0, 0, 0 ),
new int4(1590, 0, 0, 0 ),
new int4(314,  0, 0, 0 ),
new int4(51,   0, 0, 0 ),
new int4(825,  0, 0, 0 ),
new int4(560,  0, 0, 0 ),

new int4(3728, 0, 0, 0 ),
new int4(3993, 0, 0, 0 ),
new int4(3219, 0, 0, 0 ),
new int4(3482, 0, 0, 0 ),
new int4(2710, 0, 0, 0 ),
new int4(2975, 0, 0, 0 ),
new int4(2197, 0, 0, 0 ),
new int4(2460, 0, 0, 0 ),

new int4(1692, 0, 0, 0 ),
new int4(1941, 0, 0, 0 ),
new int4(1183, 0, 0, 0 ),
new int4(1430, 0, 0, 0 ),
new int4(666,  0, 0, 0 ),
new int4(915,  0, 0, 0 ),
new int4(153,  0, 0, 0 ),
new int4(400,  0, 0, 0 ),

new int4(3840, 0, 0, 0 ),
new int4(3593, 0, 0, 0 ),
new int4(3331, 0, 0, 0 ),
new int4(3082, 0, 0, 0 ),
new int4(2822, 0, 0, 0 ),
new int4(2575, 0, 0, 0 ),
new int4(2309, 0, 0, 0 ),
new int4(2060, 0, 0, 0 ),

new int4(1804, 0, 0, 0 ),
new int4(1541, 0, 0, 0 ),
new int4(1295, 0, 0, 0 ),
new int4(1030, 0, 0, 0 ),
new int4(778,  0, 0, 0 ),
new int4(515,  0, 0, 0 ),
new int4(265,  0, 0, 0 ),
new int4(0,    0, 0, 0 ),

        }, Allocator.Persistent);
        public static readonly NativeArray<float4> ambo_dist = new NativeArray<float4>(new float4[]{
new float4(0.022405551f, 0f, 0f, 0f),

new float4(0.057911754f, 0f, 0f, 0f),

new float4(0.100928018f, 0f, 0f, 0f),

new float4(0.149684838f, 0f, 0f, 0f),

new float4(0.203209744f, 0f, 0f, 0f),

new float4(0.260869218f, 0f, 0f, 0f),

new float4(0.322210685f, 0f, 0f, 0f),

new float4(0.386891248f, 0f, 0f, 0f),

new float4(0.454640229f, 0f, 0f, 0f),

new float4(0.525237377f, 0f, 0f, 0f),

new float4(0.598499239f, 0f, 0f, 0f),

new float4(0.674270145f, 0f, 0f, 0f),

new float4(0.75241599f, 0f, 0f, 0f),

new float4(0.832819782f, 0f, 0f, 0f),

new float4(0.915378376f, 0f, 0f, 0f),

new float4(1.0f, 0f, 0f, 0f),
        }, Allocator.Persistent);
        public static readonly NativeArray<float4> occlusion_amt = new NativeArray<float4>(new float4[]{
new float4(1f, 1.000000000f, 1.000000000f, 1.000000000f),

new float4(0.850997317f, 0.980824675f, 0.97451496f, 0.968245837f),

new float4(0.716176609f, 0.960732353f, 0.947988832f, 0.935414347f),

new float4(0.595056802f, 0.93960866f, 0.920299843f, 0.901387819f),

new float4(0.48713929f, 0.917314755f, 0.891301229f, 0.866025404f),

new float4(0.391905859f, 0.893679531f, 0.860813523f, 0.829156198f),

new float4(0.308816178f, 0.868488366f, 0.828613504f, 0.790569415f),

new float4(0.237304688f, 0.841466359f, 0.794417881f, 0.75f),

new float4(0.176776695f, 0.812252396f, 0.757858283f, 0.707106781f),

new float4(0.126603334f, 0.780357156f, 0.718441189f, 0.661437828f),

new float4(0.086114874f, 0.745091108f, 0.675480019f, 0.612372436f),

new float4(0.054591503f, 0.705431757f, 0.627971608f, 0.559016994f),

new float4(0.03125f, 0.659753955f, 0.574349177f, 0.5f),

new float4(0.015223103f, 0.605202038f, 0.511918128f, 0.433012702f),

new float4(0.005524272f, 0.535886731f, 0.435275282f, 0.353553391f),

new float4(0.000976563f, 0.435275282f, 0.329876978f, 0.25f),

        }, Allocator.Persistent);
        public static readonly NativeArray<float4> g_ray_dirs_256 = new NativeArray<float4>(new float4[]{
new float4(-0.146297f, -0.969372f, -0.197271f, 0f),

new float4(-0.610820f, -0.587337f, -0.530975f, 0f),

new float4(0.071929f, -0.273428f, -0.959199f, 0f),

new float4(-0.890345f, 0.093865f, -0.445506f, 0f),

new float4(-0.517643f, 0.784143f, -0.342296f, 0f),

new float4(-0.942196f, -0.071998f, -0.327235f, 0f),

new float4(-0.554498f, -0.736180f, -0.388034f, 0f),

new float4(-0.849161f, -0.338281f, 0.405575f, 0f),

new float4(-0.717542f, -0.367914f, -0.591416f, 0f),

new float4(0.115098f, 0.653731f, -0.747923f, 0f),

new float4(-0.380272f, -0.774308f, -0.505807f, 0f),

new float4(-0.341213f, -0.449520f, -0.825533f, 0f),

new float4(-0.377783f, -0.880289f, -0.287003f, 0f),

new float4(-0.436888f, -0.278887f, 0.855191f, 0f),

new float4(0.030665f, -0.973211f, 0.227859f, 0f),

new float4(-0.728434f, 0.379481f, -0.570419f, 0f),

new float4(-0.779945f, -0.473715f, -0.409000f, 0f),

new float4(-0.144476f, -0.195378f, -0.970028f, 0f),

new float4(-0.876024f, -0.447334f, -0.180205f, 0f),

new float4(-0.880123f, -0.288508f, -0.377023f, 0f),

new float4(0.010291f, -0.903700f, -0.428043f, 0f),

new float4(-0.540831f, -0.455532f, -0.707102f, 0f),

new float4(-0.106733f, -0.409233f, -0.906166f, 0f),

new float4(-0.533049f, -0.035464f, -0.845341f, 0f),

new float4(-0.889321f, 0.444652f, 0.106737f, 0f),

new float4(-0.026449f, -0.999517f, 0.016316f, 0f),

new float4(-0.758429f, 0.179937f, -0.626425f, 0f),

new float4(-0.185770f, -0.898448f, -0.397844f, 0f),

new float4(-0.989453f, 0.053219f, 0.134722f, 0f),

new float4(0.254733f, -0.207759f, -0.944430f, 0f),

new float4(-0.616723f, -0.768496f, -0.170492f, 0f),

new float4(-0.488568f, -0.266686f, -0.830771f, 0f),

new float4(0.844313f, 0.030552f, -0.534979f, 0f),

new float4(-0.736006f, -0.622568f, -0.265904f, 0f),

new float4(-0.665275f, -0.199007f, -0.719587f, 0f),

new float4(-0.370681f, -0.878862f, 0.300328f, 0f),

new float4(0.043036f, 0.246779f, -0.968116f, 0f),

new float4(-0.329387f, -0.194814f, -0.923879f, 0f),

new float4(-0.457078f, -0.885027f, -0.088352f, 0f),

new float4(-0.989463f, 0.027914f, -0.142068f, 0f),

new float4(-0.399522f, -0.631917f, -0.664126f, 0f),

new float4(-0.184812f, -0.578644f, -0.794365f, 0f),

new float4(-0.042814f, -0.693448f, -0.719234f, 0f),

new float4(0.536110f, 0.821634f, -0.193659f, 0f),

new float4(-0.962834f, -0.223021f, -0.152353f, 0f),

new float4(-0.925758f, -0.377626f, 0.019266f, 0f),

new float4(-0.657932f, -0.750899f, 0.057247f, 0f),

new float4(-0.186623f, -0.779102f, -0.598475f, 0f),

new float4(0.012484f, -0.031416f, -0.999428f, 0f),

new float4(-0.795934f, -0.604666f, -0.029452f, 0f),

new float4(-0.939159f, 0.237352f, -0.248284f, 0f),

new float4(-0.057273f, 0.427359f, -0.902266f, 0f),

new float4(-0.373864f, 0.756868f, 0.536074f, 0f),

new float4(-0.379717f, 0.085604f, -0.921133f, 0f),

new float4(-0.080665f, 0.598387f, -0.797136f, 0f),

new float4(-0.738913f, -0.005706f, -0.673776f, 0f),

new float4(-0.577285f, 0.172515f, -0.798110f, 0f),

new float4(-0.833456f, -0.143658f, -0.533585f, 0f),

new float4(-0.206088f, -0.961344f, 0.182606f, 0f),

new float4(-0.702101f, -0.428862f, 0.568447f, 0f),

new float4(-0.438616f, 0.622514f, 0.648145f, 0f),

new float4(-0.486670f, -0.863099f, 0.134949f, 0f),

new float4(-0.186374f, 0.045541f, -0.981423f, 0f),

new float4(-0.810927f, -0.557960f, 0.176291f, 0f),

new float4(-0.880351f, 0.463577f, -0.100387f, 0f),

new float4(-0.919633f, -0.332080f, 0.209757f, 0f),

new float4(-0.944564f, -0.128240f, 0.302246f, 0f),

new float4(-0.288850f, 0.507827f, -0.811589f, 0f),

new float4(0.659872f, -0.436974f, -0.611247f, 0f),

new float4(-0.275832f, -0.961043f, -0.017694f, 0f),

new float4(-0.748752f, -0.546372f, 0.375298f, 0f),

new float4(-0.972029f, 0.230575f, -0.044656f, 0f),

new float4(-0.851728f, 0.326914f, -0.409495f, 0f),

new float4(-0.173014f, 0.832988f, 0.525544f, 0f),

new float4(-0.232628f, -0.704874f, 0.670102f, 0f),

new float4(-0.194810f, 0.260899f, -0.945506f, 0f),

new float4(0.324747f, 0.943566f, 0.064989f, 0f),

new float4(0.383540f, -0.622314f, -0.682366f, 0f),

new float4(-0.485915f, -0.752873f, 0.443926f, 0f),

new float4(0.876970f, -0.450839f, 0.166334f, 0f),

new float4(0.111820f, -0.792588f, -0.599417f, 0f),

new float4(0.073662f, -0.982000f, -0.173927f, 0f),

new float4(0.195978f, -0.649207f, -0.734930f, 0f),

new float4(0.478014f, -0.428316f, -0.766842f, 0f),

new float4(-0.469586f, 0.805503f, 0.361460f, 0f),

new float4(0.752770f, 0.297066f, -0.587443f, 0f),

new float4(0.888034f, -0.112999f, 0.445676f, 0f),

new float4(0.294651f, -0.418780f, -0.858955f, 0f),

new float4(-0.604939f, 0.792096f, 0.081446f, 0f),

new float4(-0.641173f, 0.565062f, -0.519232f, 0f),

new float4(-0.724202f, 0.081144f, 0.684797f, 0f),

new float4(0.446099f, -0.875443f, 0.185995f, 0f),

new float4(0.600549f, -0.647982f, 0.468467f, 0f),

new float4(0.357793f, -0.008298f, -0.933764f, 0f),

new float4(0.230313f, -0.870440f, -0.435075f, 0f),

new float4(0.516870f, 0.125545f, -0.846808f, 0f),

new float4(-0.990374f, -0.129468f, 0.048959f, 0f),

new float4(0.172957f, 0.078491f, -0.981797f, 0f),

new float4(-0.125205f, -0.907927f, 0.399991f, 0f),

new float4(0.093947f, -0.505180f, -0.857885f, 0f),

new float4(0.350966f, 0.870856f, -0.344140f, 0f),

new float4(0.250077f, -0.938738f, -0.237133f, 0f),

new float4(-0.636658f, -0.718576f, 0.279848f, 0f),

new float4(0.707501f, 0.658509f, -0.256532f, 0f),

new float4(-0.947981f, 0.261361f, 0.181719f, 0f),

new float4(0.977786f, 0.165092f, 0.129152f, 0f),

new float4(-0.827811f, -0.170282f, 0.534540f, 0f),

new float4(0.662787f, -0.730065f, -0.166491f, 0f),

new float4(-0.798132f, 0.511301f, 0.318680f, 0f),

new float4(-0.388033f, 0.327124f, -0.861638f, 0f),

new float4(-0.567913f, 0.376653f, -0.731852f, 0f),

new float4(0.101559f, 0.911343f, -0.398924f, 0f),

new float4(0.632754f, -0.039066f, -0.773367f, 0f),

new float4(0.273842f, -0.830491f, 0.485072f, 0f),

new float4(0.468485f, -0.791965f, 0.391551f, 0f),

new float4(-0.509383f, 0.091246f, 0.855689f, 0f),

new float4(0.962405f, -0.197340f, 0.186639f, 0f),

new float4(-0.381970f, -0.069815f, 0.921534f, 0f),

new float4(-0.094935f, 0.769975f, -0.630972f, 0f),

new float4(-0.283289f, -0.811647f, 0.510859f, 0f),

new float4(0.324663f, 0.934462f, -0.146202f, 0f),

new float4(0.636131f, -0.255308f, -0.728117f, 0f),

new float4(0.516348f, 0.317899f, -0.795189f, 0f),

new float4(0.303302f, -0.563913f, 0.768121f, 0f),

new float4(0.064861f, 0.600783f, 0.796777f, 0f),

new float4(0.934945f, 0.354603f, 0.011584f, 0f),

new float4(0.599929f, -0.263587f, 0.755385f, 0f),

new float4(-0.839018f, 0.044175f, 0.542308f, 0f),

new float4(-0.576568f, -0.609378f, 0.544268f, 0f),

new float4(-0.136822f, 0.702676f, 0.698231f, 0f),

new float4(0.575464f, -0.731307f, -0.366104f, 0f),

new float4(0.247412f, -0.928133f, 0.278129f, 0f),

new float4(0.930980f, -0.365040f, -0.004737f, 0f),

new float4(-0.137047f, 0.952564f, -0.271735f, 0f),

new float4(0.258482f, 0.794614f, 0.549341f, 0f),

new float4(0.639266f, 0.685084f, 0.349284f, 0f),

new float4(0.211322f, -0.975776f, 0.056612f, 0f),

new float4(0.318313f, 0.250855f, -0.914193f, 0f),

new float4(-0.874668f, 0.294089f, 0.385314f, 0f),

new float4(0.562372f, -0.612724f, -0.555254f, 0f),

new float4(0.396344f, 0.471916f, 0.787532f, 0f),

new float4(-0.145134f, 0.872256f, -0.467017f, 0f),

new float4(0.321011f, 0.318163f, 0.892034f, 0f),

new float4(0.787824f, 0.527858f, 0.317331f, 0f),

new float4(-0.405789f, 0.471159f, 0.783163f, 0f),

new float4(-0.098401f, 0.396636f, 0.912687f, 0f),

new float4(-0.367297f, 0.925338f, -0.094033f, 0f),

new float4(-0.034038f, -0.769348f, 0.637923f, 0f),

new float4(-0.771734f, 0.627828f, 0.101285f, 0f),

new float4(-0.729669f, 0.675000f, -0.109358f, 0f),

new float4(0.589991f, -0.807313f, 0.012471f, 0f),

new float4(0.388950f, -0.920586f, -0.035209f, 0f),

new float4(0.345023f, 0.068954f, 0.936058f, 0f),

new float4(-0.242943f, 0.909735f, 0.336691f, 0f),

new float4(0.748791f, -0.448278f, 0.488220f, 0f),

new float4(0.845689f, 0.073015f, 0.528658f, 0f),

new float4(-0.925687f, 0.080180f, 0.369695f, 0f),

new float4(0.465158f, -0.193038f, -0.863924f, 0f),

new float4(-0.631773f, 0.612359f, 0.475268f, 0f),

new float4(0.187536f, 0.416349f, -0.889654f, 0f),

new float4(-0.210139f, 0.559672f, 0.801629f, 0f),

new float4(0.550192f, 0.728876f, -0.407466f, 0f),

new float4(0.996916f, -0.066042f, 0.042395f, 0f),

new float4(-0.286921f, 0.694949f, -0.659334f, 0f),

new float4(-0.115827f, 0.990572f, -0.073149f, 0f),

new float4(0.454336f, 0.494218f, -0.741166f, 0f),

new float4(0.458039f, -0.425545f, 0.780456f, 0f),

new float4(0.151344f, 0.963624f, 0.220283f, 0f),

new float4(0.500898f, 0.186769f, 0.845114f, 0f),

new float4(0.107944f, 0.972019f, -0.208633f, 0f),

new float4(-0.652615f, 0.700118f, 0.289705f, 0f),

new float4(0.248968f, 0.643234f, 0.724062f, 0f),

new float4(-0.263131f, -0.532042f, 0.804794f, 0f),

new float4(0.885901f, -0.401121f, -0.232983f, 0f),

new float4(0.052925f, 0.785420f, 0.616696f, 0f),

new float4(-0.019718f, -0.590231f, 0.806994f, 0f),

new float4(0.391274f, -0.772967f, -0.499426f, 0f),

new float4(-0.689178f, -0.235314f, 0.685318f, 0f),

new float4(0.070530f, -0.883388f, 0.463305f, 0f),

new float4(0.894994f, -0.212415f, -0.392258f, 0f),

new float4(0.024959f, 0.905451f, 0.423716f, 0f),

new float4(0.539009f, 0.842165f, 0.015114f, 0f),

new float4(0.784304f, -0.153653f, -0.601047f, 0f),

new float4(0.749910f, 0.416198f, 0.514212f, 0f),

new float4(-0.066084f, 0.975059f, 0.211878f, 0f),

new float4(0.711350f, 0.141087f, -0.688531f, 0f),

new float4(-0.237284f, -0.229566f, 0.943926f, 0f),

new float4(0.634071f, 0.469429f, -0.614484f, 0f),

new float4(0.455093f, -0.858639f, -0.235857f, 0f),

new float4(0.810347f, -0.569493f, -0.137894f, 0f),

new float4(0.281085f, 0.572318f, -0.770353f, 0f),

new float4(0.467322f, 0.855668f, 0.222358f, 0f),

new float4(-0.479159f, 0.561680f, -0.674479f, 0f),

new float4(0.899430f, 0.366544f, 0.238057f, 0f),

new float4(0.053059f, 0.085153f, 0.994954f, 0f),

new float4(-0.101088f, -0.023409f, 0.994602f, 0f),

new float4(0.502905f, -0.082002f, 0.860443f, 0f),

new float4(-0.011270f, -0.231707f, 0.972720f, 0f),

new float4(0.717275f, 0.544097f, -0.435288f, 0f),

new float4(0.943801f, 0.274985f, -0.183364f, 0f),

new float4(0.372459f, -0.257429f, 0.891630f, 0f),

new float4(0.774204f, -0.630082f, 0.060039f, 0f),

new float4(0.615060f, 0.563085f, 0.551939f, 0f),

new float4(0.884149f, 0.243162f, 0.398939f, 0f),

new float4(0.161637f, -0.720281f, 0.674588f, 0f),

new float4(-0.534952f, -0.399777f, 0.744315f, 0f),

new float4(0.839039f, 0.539975f, 0.066640f, 0f),

new float4(-0.815914f, 0.500656f, -0.289186f, 0f),

new float4(-0.430448f, -0.576774f, 0.694296f, 0f),

new float4(-0.660458f, 0.267731f, 0.701509f, 0f),

new float4(0.581747f, 0.367574f, 0.725576f, 0f),

new float4(0.108626f, 0.433703f, 0.894484f, 0f),

new float4(0.447834f, 0.627317f, 0.637117f, 0f),

new float4(-0.616730f, 0.469934f, 0.631511f, 0f),

new float4(0.953566f, -0.036920f, -0.298911f, 0f),

new float4(0.172483f, -0.300881f, 0.937934f, 0f),

new float4(0.884339f, -0.308939f, 0.350001f, 0f),

new float4(0.969583f, -0.199823f, -0.141350f, 0f),

new float4(0.468685f, 0.657808f, -0.589596f, 0f),

new float4(0.991123f, 0.081950f, -0.104690f, 0f),

new float4(-0.248249f, 0.111137f, 0.962300f, 0f),

new float4(0.851344f, 0.487126f, -0.194734f, 0f),

new float4(0.640849f, -0.734599f, 0.222883f, 0f),

new float4(-0.679681f, 0.653734f, -0.332663f, 0f),

new float4(0.764234f, -0.272639f, 0.584478f, 0f),

new float4(0.799646f, -0.375386f, -0.468670f, 0f),

new float4(-0.425323f, 0.740224f, -0.520738f, 0f),

new float4(0.465739f, 0.768163f, 0.439333f, 0f),

new float4(0.683761f, 0.713813f, 0.151465f, 0f),

new float4(0.120624f, 0.803807f, -0.582532f, 0f),

new float4(-0.469598f, 0.287652f, 0.834706f, 0f),

new float4(-0.249359f, 0.963955f, 0.092798f, 0f),

new float4(0.315179f, 0.782677f, -0.536730f, 0f),

new float4(0.397742f, -0.682759f, 0.612896f, 0f),

new float4(0.139002f, -0.483048f, 0.864490f, 0f),

new float4(0.093224f, 0.995614f, 0.007850f, 0f),

new float4(-0.148915f, -0.411344f, 0.899233f, 0f),

new float4(0.740358f, 0.240023f, 0.627900f, 0f),

new float4(0.956862f, 0.031965f, 0.288780f, 0f),

new float4(-0.771180f, 0.362731f, 0.523170f, 0f),

new float4(0.737640f, -0.564751f, -0.370059f, 0f),

new float4(0.121840f, 0.242720f, 0.962415f, 0f),

new float4(0.910476f, 0.158365f, -0.382040f, 0f),

new float4(-0.434589f, 0.885937f, 0.162011f, 0f),

new float4(0.730168f, -0.090203f, 0.677287f, 0f),

new float4(0.583693f, -0.506392f, 0.634721f, 0f),

new float4(0.763130f, -0.568421f, 0.307456f, 0f),

new float4(0.653905f, 0.078625f, 0.752481f, 0f),

new float4(-0.250918f, 0.292720f, 0.922689f, 0f),

new float4(0.729536f, 0.681624f, -0.056266f, 0f),

new float4(0.843978f, 0.361604f, -0.396161f, 0f),

new float4(-0.332027f, 0.883253f, -0.331093f, 0f),

new float4(-0.549484f, 0.825711f, -0.127547f, 0f),

new float4(-0.609998f, -0.085904f, 0.787733f, 0f),

new float4(0.212334f, -0.079420f, 0.973964f, 0f),

new float4(0.277294f, 0.890744f, 0.360115f, 0f),

        }, Allocator.Persistent);
        public static readonly NativeArray<float4> g_ray_dirs_32 = new NativeArray<float4>(new float4[]{
new float4(0.286582f, 0.257763f, -0.922729f, 0f),

new float4(-0.171812f, -0.888079f, 0.426375f, 0f),

new float4(0.440764f, -0.502089f, -0.744066f, 0f),

new float4(-0.841007f, -0.428818f, -0.329882f, 0f),

new float4(-0.380213f, -0.588038f, -0.713898f, 0f),

new float4(-0.055393f, -0.207160f, -0.976738f, 0f),

new float4(-0.901510f, -0.077811f, 0.425706f, 0f),

new float4(-0.974593f, 0.123830f, -0.186643f, 0f),

new float4(0.208042f, -0.524280f, 0.825741f, 0f),

new float4(0.258429f, -0.898570f, -0.354663f, 0f),

new float4(-0.262118f, 0.574475f, -0.775418f, 0f),

new float4(0.735212f, 0.551820f, 0.393646f, 0f),

new float4(0.828700f, -0.523923f, -0.196877f, 0f),

new float4(0.788742f, 0.005727f, -0.614698f, 0f),

new float4(-0.696885f, 0.649338f, -0.304486f, 0f),

new float4(-0.625313f, 0.082413f, -0.776010f, 0f),

new float4(0.358696f, 0.928723f, 0.093864f, 0f),

new float4(0.188264f, 0.628978f, 0.754283f, 0f),

new float4(-0.495193f, 0.294596f, 0.817311f, 0f),

new float4(0.818889f, 0.508670f, -0.265851f, 0f),

new float4(0.027189f, 0.057757f, 0.997960f, 0f),

new float4(-0.188421f, 0.961802f, -0.198582f, 0f),

new float4(0.995439f, 0.019982f, 0.093282f, 0f),

new float4(-0.315254f, -0.925345f, -0.210596f, 0f),

new float4(0.411992f, -0.877706f, 0.244733f, 0f),

new float4(0.625857f, 0.080059f, 0.775818f, 0f),

new float4(-0.243839f, 0.866185f, 0.436194f, 0f),

new float4(-0.725464f, -0.643645f, 0.243768f, 0f),

new float4(0.766785f, -0.430702f, 0.475959f, 0f),

new float4(-0.446376f, -0.391664f, 0.804580f, 0f),

new float4(-0.761557f, 0.562508f, 0.321895f, 0f),

new float4(0.344460f, 0.753223f, -0.560359f, 0f),
        }, Allocator.Persistent);
        public static readonly NativeArray<float4> g_ray_dirs_64 = new NativeArray<float4>(new float4[]{
new float4(0.635469f, -0.182706f, 0.750199f, 0f),

new float4(-0.183220f, 0.162702f, 0.969515f, 0f),

new float4(0.661016f, 0.255982f, 0.705359f, 0f),

new float4(0.946302f, -0.241407f, 0.215023f, 0f),

new float4(-0.802715f, -0.522212f, 0.288000f, 0f),

new float4(-0.372182f, -0.679187f, -0.632603f, 0f),

new float4(-0.451278f, 0.093611f, -0.887460f, 0f),

new float4(0.009654f, -0.996699f, 0.080612f, 0f),

new float4(-0.312404f, -0.914132f, -0.258391f, 0f),

new float4(-0.798658f, -0.132614f, 0.586992f, 0f),

new float4(-0.783280f, -0.325238f, -0.529804f, 0f),

new float4(-0.700495f, -0.672603f, -0.238562f, 0f),

new float4(-0.937567f, 0.319168f, -0.138202f, 0f),

new float4(-0.483988f, 0.610024f, 0.627397f, 0f),

new float4(-0.091554f, -0.317586f, -0.943799f, 0f),

new float4(0.124272f, -0.933613f, -0.336040f, 0f),

new float4(0.210419f, -0.874501f, 0.437004f, 0f),

new float4(-0.944818f, -0.297149f, -0.137920f, 0f),

new float4(0.736483f, -0.498476f, 0.457292f, 0f),

new float4(-0.496048f, -0.292138f, -0.817675f, 0f),

new float4(0.066274f, -0.713414f, -0.697601f, 0f),

new float4(0.364059f, -0.465295f, -0.806822f, 0f),

new float4(-0.896433f, 0.304377f, 0.322123f, 0f),

new float4(-0.858053f, 0.088162f, -0.505938f, 0f),

new float4(-0.982615f, -0.074319f, 0.170131f, 0f),

new float4(0.163698f, -0.002378f, -0.986508f, 0f),

new float4(0.179198f, 0.713624f, -0.677221f, 0f),

new float4(-0.698601f, 0.669385f, 0.252745f, 0f),

new float4(-0.277605f, -0.846333f, 0.454594f, 0f),

new float4(0.105158f, 0.984492f, 0.140420f, 0f),

new float4(-0.312357f, 0.944995f, -0.097040f, 0f),

new float4(0.840329f, 0.459466f, -0.287643f, 0f),

new float4(-0.070270f, -0.635150f, 0.769186f, 0f),

new float4(-0.668568f, 0.725715f, -0.162341f, 0f),

new float4(0.778421f, 0.199311f, -0.595261f, 0f),

new float4(-0.000185f, 0.364384f, -0.931249f, 0f),

new float4(0.583553f, -0.101756f, -0.805674f, 0f),

new float4(-0.270049f, 0.891938f, 0.362657f, 0f),

new float4(0.688168f, -0.510370f, -0.515701f, 0f),

new float4(0.452752f, 0.395566f, -0.799089f, 0f),

new float4(0.288105f, 0.451597f, 0.844426f, 0f),

new float4(0.497123f, -0.818258f, -0.288656f, 0f),

new float4(0.821143f, -0.568639f, -0.048725f, 0f),

new float4(0.993292f, 0.102991f, -0.052567f, 0f),

new float4(0.523924f, -0.835196f, 0.167187f, 0f),

new float4(0.582061f, 0.655171f, 0.481618f, 0f),

new float4(0.822215f, 0.553139f, 0.134163f, 0f),

new float4(-0.530595f, -0.836226f, 0.138543f, 0f),

new float4(-0.700680f, 0.483009f, -0.525119f, 0f),

new float4(0.400960f, -0.576978f, 0.711567f, 0f),

new float4(-0.629533f, 0.235898f, 0.740298f, 0f),

new float4(0.547155f, 0.738837f, -0.393371f, 0f),

new float4(-0.374241f, 0.483409f, -0.791366f, 0f),

new float4(0.046768f, -0.300501f, 0.952634f, 0f),

new float4(0.176532f, 0.817491f, 0.548219f, 0f),

new float4(0.286203f, 0.018724f, 0.957986f, 0f),

new float4(-0.417591f, -0.182567f, 0.890105f, 0f),

new float4(0.128945f, 0.942822f, -0.307342f, 0f),

new float4(0.913928f, -0.188092f, -0.359664f, 0f),

new float4(-0.539456f, -0.524214f, 0.658928f, 0f),

new float4(-0.274778f, 0.804161f, -0.527089f, 0f),

new float4(0.503647f, 0.862273f, 0.053150f, 0f),

new float4(-0.120169f, 0.565524f, 0.815930f, 0f),

new float4(0.902855f, 0.162083f, 0.398225f, 0f),
        }, Allocator.Persistent);
        public static readonly NativeArray<float4> g_ray_dirs = g_ray_dirs_32;

        public static void Dispose()
        {
            foreach (var item in typeof(MarchingCubeConstantBuffer).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                object value = item.GetValue(null);
                if (value != null && typeof(IDisposable).IsAssignableFrom(value.GetType()))
                {
                    try
                    {
                        ((IDisposable)value).Dispose();
                    }
                    catch (ObjectDisposedException) {}
                }
            }
        }
    }
}