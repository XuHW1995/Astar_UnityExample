# Astar_UnityExample
A星算法untiy练习实现

在一个目标地图内，鼠标左键点击地图上某一点，角色自动按照指定路线移动  

要求：  
1.静态寻路，不包含动态障碍物  
2.障碍物随机  
3.行动路线标识  
4.切换目标点刷新路线并移动  
5. 支持对角运动和非对角运动切换  

需求分解：  
1.构建地图信息（✔）  
2.随机生成障碍物（✔）   
3.重新获取地图数据（✔）    
4.Astar实现（✔）  
5.点击位置获取（✔）  
6.移动   
7.显示寻路路径（✔）  

新增：  
1.策略模式，不同情况选择不同寻路方式    

2.无法到达点的寻路策略：返回一条距离目标点最近的点的寻路结果  

3.手动设置障碍物结点

