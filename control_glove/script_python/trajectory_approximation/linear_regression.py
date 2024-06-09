import seaborn as sns
import matplotlib.pyplot as plt

iris = sns.load_dataset('iris')
iris = iris[['petal_length','petal_width']]
x = iris['petal_length']
y = iris['petal_width']

plt.scatter(x,y)
plt.xlabel("petal length")
plt.show()
# print(iris)