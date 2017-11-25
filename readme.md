Beacon -  A marine loading and path detection system
======================================================

The primary goal of the project is to resolved logical and mathematical complexities for marine companies. The project provides a new
approach of solution to two popularly known NP-hard problems i.e. TSP(Travelling Salesman Problem) and CLP(Container Loading Problem).

### Root Planner ###
![Alt text](https://github.com/binit92/Beacon/blob/master/rootplanner.PNG)

As we know that travelling salesman problem cannot be solved in polynomial time when there are more than 25 nodes. So, we need to use approximation solutions thats fits right in. Genetic algorithms mimics nature and evolution using the principles of survival of the fittest. Although it might not find the best solution, it can find a near perfect solution for a 100 city tour in less than a minute. Greedy approaches can be used in between the Genetic algorithm to map closet cities in the initial population generation

### Load Planner ###
![Alt text](https://github.com/binit92/Beacon/blob/master/loadplanner.PNG)

2D-Bin-Packing works on pack a given set of 2D-rectangles into unit square bins so that the number of bins is minimised. Even very simple cases of these problem are known to be NP-hard, and hence, it is very likely that no efficient algorithms for them exist.In the design of such algorithms, a simple shelf technique is used: order the rectangles according to a sorting rule like decreasing width, increasing height, etc., and then greedily pack them one by one in this order over package shelves according to some rule, like First-Fit, Next-Fit, Best-Fit, Worst-Fit and so on.


[Final Documentation](https://github.com/binit92/Beacon/blob/master/FYP/final%20year%20project%20-%20binit%20kumar-x1/Documentation/Documentation.pdf)


[Research paper](https://github.com/binit92/Beacon/blob/master/FYP/final%20year%20project%20-%20binit%20kumar-x1/Documentation/Research%20Paper.pdf)


Author:
kumar.binit1992@gmail.com

