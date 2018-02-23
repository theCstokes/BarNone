import json
from pprint import pprint
import matplotlib.pyplot as plt
import numpy as np
import pandas
from scipy.signal import butter, lfilter, freqz
from mpl_toolkits.mplot3d import Axes3D
from fastdtw import fastdtw


JOINT_MAPPINGS = [
    "Ass",
    "Middle of the spine",
    "Neck",
    "Head",
    "Left Shoulder",
    "Left Elbow",
    "Left Wrist",
    "Left Hand",
    "Right Shoulder",
    "Right Elbow",
    "Right Wrist",
    "Right Hand",
    "Left Hip",
    "Left Knee",
    "Left Ankle",
    "Left Foot",
    "Right Hip",
    "Right Knee",
    "Right Ankle",
    "Right Foot",
    "Spine at the Shoulder"
]

#https://stackoverflow.com/questions/25191620/creating-lowpass-filter-in-scipy-understanding-methods-and-units
def butter_lowpass(cutoff, fs, order=5):
    nyq = 0.5 * fs
    normal_cutoff = cutoff / nyq
    b, a = butter(order, normal_cutoff, btype='low', analog=False)
    return b, a

def butter_lowpass_filter(data, cutoff, fs, order=5):
    b, a = butter_lowpass(cutoff, fs, order=order)
    y = lfilter(b, a, data)
    return y

data = json.load(open('Chris_Single_Squat_1.json'))
# data = json.load(open('SQUAT_2018_01_13.json'))
frames = data["Details"]["BodyData"]["Details"]["OrderedFrames"]
joint_data = frames[0]["Details"]["Joints"][0]


xs = np.array([joint["Details"]["Joints"][0]['X'] for joint in frames])


# Filter requirements.
order = 2
fs = 20.0      # sample rate, Hz
cutoff = 2.0  # desired cutoff frequency of the filter, Hz

# Get the filter coefficients so we can check its frequency response.
b, a = butter_lowpass(cutoff, fs, order)

# Plot the frequency response.
w, h = freqz(b, a, worN=8000)
plt.subplot(2, 1, 1)
plt.plot(0.5*fs*w/np.pi, np.abs(h), 'b')
plt.plot(cutoff, 0.5*np.sqrt(2), 'ko')
plt.axvline(cutoff, color='k')
plt.xlim(0, 0.5*fs)
plt.title("Lowpass Filter Frequency Response")
plt.xlabel('Frequency [Hz]')
plt.grid()


# Demonstrate the use of the filter.
# First make some data to be filtered.
T = 5.0         # seconds
n = int(T * fs) # total number of samples
t = np.linspace(0, T, n, endpoint=False)
# "Noisy" data.  We want to recover the 1.2 Hz signal from this.
data = np.sin(1.2*2*np.pi*t) + 1.5*np.cos(9*2*np.pi*t) + 0.5*np.sin(12.0*2*np.pi*t)

# Filter the data, and plot both the original and filtered signals.
y = butter_lowpass_filter(data, cutoff, fs, order)

plt.subplot(2, 1, 2)
plt.plot(t, data, 'b-', label='data')
plt.plot(t, y, 'g-', linewidth=2, label='filtered data')
plt.xlabel('Time [sec]')
plt.grid()
plt.legend()

plt.subplots_adjust(hspace=0.35)
plt.show()




for i in range(0, 21):
    xs = np.array([joint["Details"]["Joints"][i]['X'] for joint in frames])
    height = np.array([joint["Details"]["Joints"][i]['Y'] for joint in frames])
    depth = np.array([joint["Details"]["Joints"][i]['Z'] for joint in frames])


    h0 = height[0]
    height = height - h0

    height_filtered = butter_lowpass_filter(height,cutoff,fs,2)
    height = height + h0
    height_filtered = height_filtered + h0

    height_ts = pandas.Series(height)
    height_ts.plot()
    pandas.Series(height_filtered).plot()


    fig = plt.figure()

    d0 = depth[0]
    depth = depth - d0

    depth_fitlered = butter_lowpass_filter(depth,cutoff,fs,2)
    depth = depth + d0
    depth_fitlered = depth_fitlered + d0


    depth_ts = pandas.Series(depth)
    depth_ts.plot()
    pandas.Series(depth_fitlered).plot()

    fig = plt.figure()

    ax = fig.gca(projection='3d')

    ax.plot(xs,depth,height)
    ax.set_title(JOINT_MAPPINGS[i])
    ax.set_xlabel('X Label')
    ax.set_ylabel('Depth')
    ax.set_zlabel('Height')
    plt.show()
    print(i)
