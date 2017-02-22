// http://www.codiply.com/blog/topic-specific-pagerank-simulation-in-f-sharp/
// https://gist.github.com/sebastien-bratieres/0295aaa9a4d4acab4a0d

#load "../packages/FsLab.1.0.2/FsLab.fsx"

open System
open MathNet.Numerics
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.Distributions

let nPages = 11 // numbering pages A through K as 0 to 10

let mCounts = DenseMatrix.zero<float> nPages nPages // will hold the number of link counts (assumed 0 or 1)

for i in 0..nPages-1 do
    mCounts.[i, 0]  <- 1.0 // sum of columns

mCounts.[2,1] <- 1.0 // B->C
mCounts.[1,2] <- 1.0 // C->B
mCounts.[0,3] <- 1.0 // D->A
mCounts.[1,3] <- 1.0 // D->B
mCounts.[1,4] <- 1.0 // E->B
mCounts.[3,4] <- 1.0 // E->D
mCounts.[5,4] <- 1.0 // E->F
mCounts.[1,5] <- 1.0 // F->B
mCounts.[4,5] <- 1.0 // F->E
mCounts.[1,6] <- 1.0 // G,H,I->B,E
mCounts.[4,6] <- 1.0
mCounts.[1,7] <- 1.0
mCounts.[4,7] <- 1.0
mCounts.[1,8] <- 1.0
mCounts.[4,8] <- 1.0
mCounts.[4,9] <- 1.0 // J,K->E
mCounts.[4,10] <- 1.0

//let mCounts = matrix[[ 1.;   0.;   0.;   1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   1.;   1.;   1.;   1.;   1.;   1.;   1.;   0.;   0.];  
// [ 1.;   1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   1.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   1.;   1.;   1.;   1.;   1.;   1.];  
// [ 1.;   0.;   0.;   0.;   1.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  
// [ 1.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.;   0.];  ];;

for i in 0..nPages-1 do
  for j in 0..nPages-1 do
    mCounts.[j, i]  <- mCounts.[j, i] / mCounts.[0..nPages-1, i].Sum() // sum of columns

let pageRank M = 
  let d = 0.85
  let squareError = 0.00000001
  
let rnd = System.Random()

let v = vector [for i in 0..nPages -> rnd.NextDouble()]

let x = vector [ v / v.Sum() ]

let lastv = vector [for i in 0..nPages -> 1.0]
