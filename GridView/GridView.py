import matplotlib.pyplot as plt

def read_points(data):
    points = {}
    lines = data.split('\n')[1:]
    for line in lines:
        if line.strip():
            parts = line.replace(',', '.').split()
            global_id = int(parts[0])
            x = float(parts[1])
            y = float(parts[2])
            points[global_id] = (x, y)
    return points

def read_connections(connection_data):
    connections = []
    lines = connection_data.split('\n')
    for line in lines:
        if line.strip():
            parts = line.split()
            point1 = int(parts[1])
            point2 = int(parts[2])
            connections.append((point1, point2))
    return connections

def plot_points_and_connections(points, connections):
    plt.figure(figsize=(8, 6))

    for point_id, (x, y) in points.items():
        plt.scatter(x, y, label=f'Point {point_id}')
        plt.text(x, y, f'{point_id}', fontsize=12, ha='right')

    for point1, point2 in connections:
        x_values = [points[point1][0], points[point2][0]]
        y_values = [points[point1][1], points[point2][1]]
        plt.plot(x_values, y_values, 'b-')

    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title('Points and Connections')
    plt.grid(True)
    plt.legend()
    plt.show()

point_data = """77
0 0 0
1 1 0
2 3 0
3 4 0
4 5 0
5 8 0
6 0 3
7 2 3
8 3 3
9 4 3
10 5 3
11 8 3
12 0 5
13 2 5
14 3 5
15 4 5
16 5 5
17 8 5
18 0 7
19 2 7
20 3 7
21 4 7
22 7 7
23 8 7
"""

connection_data = """
2 0 1 6 7
2 1 2 7 8
2 2 3 8 9
2 3 4 9 10
2 4 5 10 11
1 6 7 12 13
1 7 8 13 14
2 8 9 14 15
2 9 10 15 16
2 10 11 16 17
1 12 13 18 19
1 13 14 19 20
2 14 15 20 21
2 15 16 21 22
2 16 17 22 23
"""

points = read_points(point_data)
connections = read_connections(connection_data)

plot_points_and_connections(points, connections)