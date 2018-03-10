#DEFINE f = x^2+3*x+10;

int numSteps = (b-a) / n;

float result = 0;

for(int i = 0; i < numSteps; i++) {
    result += f((rand() % (b-a)) + a)*(b-a);
}

result /= numSteps;