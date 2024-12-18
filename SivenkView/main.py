﻿import matplotlib.pyplot as plt
import numpy as np
from scipy.interpolate import griddata
from matplotlib.patches import Polygon
from matplotlib.collections import LineCollection

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
        materialId = int(line[0])
        point1 = int(line[1])
        point2 = int(line[2])
        point3 = int(line[3])
        point4 = int(line[4])
        connections.append((materialId, point1, point2, point3, point4))
    return connections

def draw_polygon(ax, coords, color):
    poly = Polygon(coords, closed=True)
    poly.set_facecolor(color)
    ax.add_patch(poly)

def plot_points_and_connections(points, connections):
    ax = fig.add_subplot(111)
    ax.set_aspect('equal', 'datalim')

    x_coords = [x for x, y in points.values()]
    y_coords = [y for x, y in points.values()]
    plt.scatter(x_coords, y_coords, color='black')

    lines = []
    for materialId, point1, point2, point3, point4 in connections:
        lines.extend([
            (points[point1], points[point2]),
            (points[point1], points[point3]),
            (points[point3], points[point4]),
            (points[point2], points[point4])
        ])

        coords = [points[point1], points[point3], points[point4], points[point2]]
        draw_polygon(ax, coords, (1.0, 0.5, 0.0, 0.0))

    line_collection = LineCollection(lines, colors='black', linewidths=0.8)
    ax.add_collection(line_collection)

file_path = R'C:\Users\dotad\RiderProjects\Sivenk\Sivenk\output\Result.txt'
points_path = R'C:\Users\dotad\RiderProjects\Sivenk\Sivenk\output\points.txt'
elements_path = R'C:\Users\dotad\RiderProjects\Sivenk\Sivenk\output\elements.txt'

first_value, point_data = read_file(points_path)
first_value, connection_data = read_file(elements_path)

points = read_points(point_data)
connections = read_connections(connection_data)

data = np.loadtxt(file_path, usecols=(0, 1, 2))
x_coords, y_coords, values = data[:, 0], data[:, 1], data[:, 2]

grid_x, grid_y = np.mgrid[
    min(x_coords):max(x_coords):200j,
    min(y_coords):max(y_coords):200j
]

grid_z = griddata((x_coords, y_coords), values, (grid_x, grid_y), method="cubic")

fig = plt.figure(figsize=(8, 6))

plot_points_and_connections(points, connections)

plt.imshow(
    grid_z.T,
    extent=(
        min(x_coords),
        max(x_coords),
        min(y_coords),
        max(y_coords)
    ),
    origin="lower",
    cmap="plasma",
    vmin=min(values),
    vmax=max(values)
)

plt.colorbar(label="Function value")
plt.title("Field")
plt.xlabel("X")
plt.ylabel("Y")
plt.tight_layout()
plt.show()