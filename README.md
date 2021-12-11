# Group Simulation

## About

This project aims to simulate small group decision making processes using machine learning.

## Getting Started

### Installing

Before getting started, make sure you have [Unity](https://unity.com/download) and [Python](https://www.python.org/downloads/) installed. This project uses Unity Version `2020.3.20f` and Python version `3.9.6`. All necessary Python packages are in the `requirements.txt` file. To install the packages, run the following command in the root of the project directory.

```
pip install -r requirements.txt
```

### Running

To train the agents, navigate into the project directory and run the following command.

```
mlagents-learn config\GroupSimulation.yaml --run-id=<run-identifier>
```

This will start a Python server which Unity can connect to. You will need to open Unity and press the "play" button when prompted. Once the training is finished, you can view the results using Tensorboard.

```
tensorboard --logdir=<results-directory> --port=<port-number>
```

## Documentation

-   [Unity Machine Learning Agents Toolkit](https://github.com/Unity-Technologies/ml-agents/)
-   [Unity ML-Agents API](https://docs.unity3d.com/Packages/com.unity.ml-agents@2.0/api/Unity.MLAgents.html)

## License

Distributed under the MIT License.
