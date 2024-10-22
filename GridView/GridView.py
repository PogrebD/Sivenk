import matplotlib.pyplot as plt


def read_file(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()

    first_line = int(lines[0].strip())
    data = []

    for line in lines[1:]:
        numbers = list(map(float, line.split()))
        data.append(numbers)
    return first_line, data


def read_points(data):
    points = {}

    for line in data:
        global_id = int(line[0])
        x = float(line[1])
        y = float(line[2])
        points[global_id] = (x, y)
    return points


def read_connections(connection_data):
    connections = []

    for line in connection_data:
        point1 = int(line[1])
        point2 = int(line[2])
        point3 = int(line[3])
        point4 = int(line[4])
        connections.append((point1, point2, point3, point4))
    return connections


def plot_points_and_connections(points, connections):
    plt.figure(figsize=(8, 6))

    x_coords = [x for x, y in points.values()]
    y_coords = [y for x, y in points.values()]
    plt.scatter(x_coords, y_coords, color='green')

    for point_id, (x, y) in points.items():
        plt.text(x, y, f'{point_id}', fontsize=10, ha='right')

    for point1, point2, point3, point4 in connections:
        x_values = [points[point1][0], points[point2][0]]
        y_values = [points[point1][1], points[point2][1]]
        plt.plot(x_values, y_values, 'b-')

        x_values = [points[point1][0], points[point3][0]]
        y_values = [points[point1][1], points[point3][1]]
        plt.plot(x_values, y_values, 'b-')

        x_values = [points[point3][0], points[point4][0]]
        y_values = [points[point3][1], points[point4][1]]
        plt.plot(x_values, y_values, 'b-')

        x_values = [points[point2][0], points[point4][0]]
        y_values = [points[point2][1], points[point4][1]]
        plt.plot(x_values, y_values, 'b-')

    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title('Points and Connections')
    plt.grid(True)
    plt.legend()
    plt.show()


points_path = '../Sivenk/output/points.txt'
elements_path = '../Sivenk/output/elements.txt'

first_value, point_data = read_file(points_path)
first_value, connection_data = read_file(elements_path)

points = read_points(point_data)
connections = read_connections(connection_data)

plot_points_and_connections(points, connections)
