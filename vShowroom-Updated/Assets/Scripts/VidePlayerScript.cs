using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using System.IO;


public class VideoPlayerScript : MonoBehaviour
{
    public string videoFileName; // The name of the video file in StreamingAssets
   // public VisualTreeAsset visualTreeAsset; // Assign in the Inspector
    public UIDocument uiDocument;
    private VideoPlayer videoPlayer;
    private Button playButton;
    private Button pauseButton;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component missing.");
            return;
        }

        // Get the video path from StreamingAssets
        videoPlayer.url = "http://yourserver.com/path/to/your_video.mp4";

        // Check if the video file exists
      /*  if (!File.Exists(videoPath))
        {
            Debug.LogError("Video file not found: " + videoPath);
            return;
        }*/

      //  videoPlayer.url = videoPath;

        // Load the UXML
        var root = uiDocument.rootVisualElement;
        //visualTreeAsset.CloneTree(root);

        // Find the buttons
        playButton = root.Q<Button>("play-button");
        pauseButton = root.Q<Button>("pause-button");

        // Register button click events
        playButton.clicked += PlayVideo;
        pauseButton.clicked += PauseVideo;

        // Initially hide the pause button
        pauseButton.style.display = DisplayStyle.None;

        // Prepare the video
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // Display the first frame
        vp.Play();
        vp.Pause(); // Pause immediately to display the first frame
    }

    void PlayVideo()
    {
        videoPlayer.Play();
        playButton.style.display = DisplayStyle.None;
        pauseButton.style.display = DisplayStyle.Flex;
    }

    void PauseVideo()
    {
        videoPlayer.Pause();
        playButton.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;
    }

    void StopVideo()
    {
        videoPlayer.Stop();
        playButton.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            // Unregister callback
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }

        // Unregister button click events
        if (playButton != null) playButton.clicked -= PlayVideo;
        if (pauseButton != null) pauseButton.clicked -= PauseVideo;
    }
}
