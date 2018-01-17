
        #region go script
        // --------------v a separate class
/*         if (Input.GetKeyDown(KeyCode.G) && (transform.GetComponent<NavWaypointMover>() == null)) // check if it's not already attached
        {
            if (transform.parent.GetComponentInChildren<PathwayDraw>().gameObject.activeInHierarchy == true)
            {
                transform.parent.GetComponentInChildren<PathwayDraw>().gameObject.SetActive(false);
            }
            navMeshAgent = this.GetComponentInParent<NavMeshAgent>();
            NavWaypointMover m = this.gameObject.AddComponent<NavWaypointMover>();
            m.navMeshAgent = navMeshAgent;
            m.Initialize();
            Move();  // execute the move event
            return;
        } */
        //  -------------^ a separate class
        #endregion


           //----v could be separate class
#region timerExecute
/*     IEnumerator TimerExecute()
    {   
        timerRunning = true;
        while(isPaused) {yield return null;}

        CommandExecute c = this.gameObject.AddComponent<CommandExecute>();
        for (int i = 0; i < 10; i++)
        {
              while(isPaused) {yield return null;}

			if (CommandQueue[i] != null && CommandQueue[i] != null)
			{
                c.CommandSwitch();
           		print(CommandQueue[i]);
			}
              while(isPaused) {yield return null;}

                   yield return HalfSecond();

              while(isPaused) {yield return null;}
        }
        
        Destroy (c);
        print("done");
        timerRunning = false;
        yield return null;

    } */
#endregion
    //----^ could be separate class