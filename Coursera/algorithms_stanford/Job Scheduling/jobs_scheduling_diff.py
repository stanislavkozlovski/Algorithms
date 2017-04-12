class Job:
    def __init__(self, weight, length):
        self.weight = weight
        self.length = length

    def __gt__(self, other):
        if (self.weight-self.length) == (other.weight-other.length):
            return self.weight > other.weight
        return (self.weight-self.length) > (other.weight-other.length)

    def __str__(self):
        return f"weight: {self.weight} len: {self.length}"

    def __repr__(self):
        return self.__str__()


def read_input() -> [Job]:
    jobs = []
    with open('input.txt') as f:
        read_lines: [str] = f.readlines()
        for line in read_lines[1:]:
            weigth, length = [int(p) for p in line.split()]
            jobs.append(Job(weigth, length))

    return jobs

sorted_jobs = reversed(sorted(read_input()))
overall_time = 0
result = 0
for job in sorted_jobs:
    overall_time += job.length
    result += overall_time * job.weight

print(result)
