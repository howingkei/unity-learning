# 一.游戏规则与游戏要求
游戏设计要求：
- 创建一个地图和若干巡逻兵(使用动画)；
- 每个巡逻兵走一个3~5个边的凸多边型，位置数据是相对地址。即每次确定下一个目标位置，- 用自己当前位置为原点计算；
- 巡逻兵碰撞到障碍物，则会自动选下一个点为目标；
- 巡逻兵在设定范围内感知到玩家，会自动追击玩家；
- 失去玩家目标后，继续巡逻；
- 计分：玩家每次甩掉一个巡逻兵计一分，与巡逻兵碰撞游戏结束；
程序设计要求：
- 必须使用订阅与发布模式传消息
- 工厂模式生产巡逻兵

友善提示1：生成 3~5个边的凸多边型
- 随机生成矩形
- 在矩形每个边上随机找点，可得到 3 - 4 的凸多边型
友善提示2：参考以前博客，给出自己新玩法
游戏规则：
使用WSAD或方向键上下左右移动player，进入巡逻兵的追捕后逃脱可积累一分，若与巡逻兵碰撞则游戏结束。

订阅-发布模式：订阅者把自己想订阅的事件注册到调度中心，当该事件触发时候，发布者发布该事件到调度中心，由调度中心统一调度订阅者注册到调度中心的处理代码在这个游戏中，记分员类就充当发布者角色，其他涉及加分的事件对它进行订阅，这样每次加分事件发生统一由记分员完成加分，不需要各个类分别处理，从而减少耦合，达成优化目的。

# 游戏实现：
- 巡逻兵Patrols的创建。游戏要求通过工厂模式生产巡逻兵。PatrolFactory.cs：用一个链表来储存巡逻兵。
动作管理器，这部分与之前的类似，包含巡逻兵巡逻的动作以及巡逻兵追踪玩家的动作。Action.cs文件
- 场景控制器中实现了玩家的移动以及资源的加载。 玩家的移动当中需要实现两个操作，一个是让玩家平滑地进行移动，然后就是让玩家朝着移动的方向看。还有需要在update中每一帧检测玩家进入的块的编号，然后找到这个块中对应的巡逻兵去调用follow函数进行追踪。
- 订阅与发布模式的部分。通过订阅与发布模式实现，首先定义一个发布事件的类GameEventManager，订阅者订阅该类中声明的事件，当其他类发生改变的时候，会使用GameEventManager的方法发布消息。场景控制器是订阅者，订阅了GameEventManager中的事件，如果在场景中相应事件发生，那么场景控制器就会调用相应的方法，即得分操作与游戏结束操作。
- 碰撞部分。 AreaCollide.cs文件。有以下几种碰撞操作：玩家进入区域内的碰撞，玩家离开区域内的碰撞以及玩家碰到巡逻兵的碰撞。前两个碰撞实现在同一类中，挂载到区域的触发器上，当玩家进入或退出就会触发类中的函数。
- 计分器类。ScoreRecorder.cs文件。