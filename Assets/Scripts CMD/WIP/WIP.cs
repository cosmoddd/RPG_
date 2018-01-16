
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